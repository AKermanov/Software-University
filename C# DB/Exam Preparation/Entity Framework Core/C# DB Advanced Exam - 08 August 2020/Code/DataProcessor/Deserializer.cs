namespace VaporStore.DataProcessor
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Castle.Core.Internal;
    using Data;
    using Microsoft.EntityFrameworkCore.Internal;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;
    using VaporStore.DataProcessor.ImprotDto;

    public static class Deserializer
	{
		private const string ErrorMessage = "Invalid Data";
		public static string ImportGames(VaporStoreDbContext context, string jsonString)
		{
		
			StringBuilder sb = new StringBuilder();

			var gameDto = JsonConvert.DeserializeObject<ImportGameDto[]>(jsonString);

			var games = new List<Game>();
			var developers = new List<Developer>();
			var genars = new List<Genre>();
			var gamesTags = new HashSet<GameTag>();
			var tags = new List<Tag>();

            foreach (var game in gameDto)
            {
				if (!IsValid(game) || !game.Tags.Any())
				{
					sb.AppendLine(ErrorMessage);
					continue;
				}

				var currentGenr = genars.FirstOrDefault(x => x.Name == game.Genre);
				var genr2 = new Genre();

				if (currentGenr == null)
				{
					genr2.Name = game.Genre;
					genars.Add(genr2);
				}

				var currentDeveloper = developers.FirstOrDefault(x => x.Name == game.Developer);
				var developer2 = new Developer();

				
				if (currentDeveloper == null)
				{
					developer2.Name = game.Developer;
					developers.Add(developer2);
				}

				var currentGame = new Game
				{
					Name = game.Name,
					Price = game.Price,
					ReleaseDate = DateTime.ParseExact(game.ReleaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
					Developer = currentDeveloper != null ? currentDeveloper : developer2,
					Genre = currentGenr != null ? currentGenr : genr2,
				};

                foreach (var tag in game.Tags)
                {
					var currentTag = new Tag
					{
						Name = tag
					};
                
					var currentGameTag = new GameTag
					{
						Game = currentGame,
						Tag = currentTag
					};

					var isCurrentTagExist = tags.FirstOrDefault(x => x.Name == currentTag.Name);

                    if (isCurrentTagExist == null)
                  {
						tags.Add(currentTag);
						gamesTags.Add(currentGameTag);
					}
				}

			

				games.Add(currentGame);
				
				sb.AppendLine($"Added {currentGame.Name} ({currentGame.Genre.Name}) with {game.Tags.Length} tags");
			}

			context.Games.AddRange(games);
			context.Genres.AddRange(genars.Distinct());
			context.Developers.AddRange(developers.Distinct());
			context.Tags.AddRange(tags.Distinct());
			context.GameTags.AddRange(gamesTags.Distinct());
			context.SaveChanges();
			return sb.ToString().TrimEnd();

		}

		public static string ImportUsers(VaporStoreDbContext context, string jsonString)
		{
			
			StringBuilder sb = new StringBuilder();

			var importUsersDto = JsonConvert.DeserializeObject<ImportUsersAndCardsDto[]>(jsonString);
			var usersList = new List<User>();
			var cardsList = new List<Card>();

            foreach (var user in importUsersDto)
            {

                if (!IsValid(user) || !IsValidFullName(user.FullName) || user.Cards.IsNullOrEmpty())
                {
					sb.AppendLine(ErrorMessage);
					continue;
                }

				var currentUser = new User
				{
					FullName = user.FullName,
					Username = user.Username,
					Email = user.Email,
					Age = user.Age
				};

                foreach (var card in user.Cards)
                {
                    if (!ValideteTypeCard(card.Type) || !IsValid(card))
                    {
						sb.AppendLine(ErrorMessage);
						continue;
					}


					var currentCard = new Card
					{
						Number = card.Number,
						Cvc = card.CVC,
						Type = (CardType)Enum.Parse(typeof(CardType), card.Type)
					};
					cardsList.Add(currentCard);

					currentUser.Cards.Add(currentCard);
                }
				usersList.Add(currentUser);
				sb.AppendLine($"Imported {currentUser.Username} with {currentUser.Cards.Count} cards");
            }
			context.Users.AddRange(usersList);
			context.SaveChanges();
			return sb.ToString().TrimEnd();
			
		}

		public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
		{
			var sb = new StringBuilder();

			var xmlSerializer = new XmlSerializer(typeof(List<ImportPurchasesDto>), new XmlRootAttribute("Purchases"));

			var purchiseDto = (List<ImportPurchasesDto>)xmlSerializer.Deserialize(new StringReader(xmlString));

			var listOfPurchise = new List<Purchase>();

            foreach (var purchise in purchiseDto)
            {
                if (!IsValid(purchise) || !ValidetaTypePurchase(purchise.Type))
                {
					sb.AppendLine(ErrorMessage);
					continue;
                }

				var currentGame = context.Games.FirstOrDefault(x => x.Name == purchise.Title);
				var currentCard = context.Cards.FirstOrDefault(x => x.Number == purchise.CardNumber);
				var currentPurchise = new Purchase
				{
					Game = currentGame,
					ProductKey = purchise.ProductKey,
					Type = (PurchaseType)Enum.Parse(typeof(PurchaseType), purchise.Type),
					Card = currentCard,
					Date = DateTime.ParseExact(purchise.Date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
				};

				listOfPurchise.Add(currentPurchise);
				var username = currentPurchise.Card.User.Username;
				sb.AppendLine($"Imported {currentPurchise.Game.Name} for {username}");
            }

			context.Purchases.AddRange(listOfPurchise);
			context.SaveChanges();
			return sb.ToString().TrimEnd();
		}

		private static bool IsValid(object dto)
		{
			var validationContext = new ValidationContext(dto);
			var validationResult = new List<ValidationResult>();

			return Validator.TryValidateObject(dto, validationContext, validationResult, true);
		}

		private static bool IsValidFullName(string name)
		{
			var fullNames = name.Split(" ", StringSplitOptions.RemoveEmptyEntries);
			if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name) || fullNames.Length != 2)
			{
				return false;
			}

			var firsName = fullNames[0];

			for (int i = 0; i < firsName.Length; i++)
			{
				char currentChar = firsName[i];
				if (i == 0)
				{
					if (!char.IsUpper(currentChar))
					{
						return false;
					}
				}
				else
				{
					if (!char.IsLower(currentChar))
					{
						return false;
					}
				}
			}

			var secondName = fullNames[1];

			for (int i = 0; i < secondName.Length; i++)
			{
				char currentChar = secondName[i];
				if (i == 0)
				{
					if (!char.IsUpper(currentChar))
					{
						return false;
					}
				}
				else
				{
					if (!char.IsLower(currentChar))
					{
						return false;
					}
				}
			}
			return true;
		}

		private static bool ValideteTypeCard(string type)
		{
			if (type == "Debit" || type == "Credit")
			{
				return true;
			}
			return false;

		}

		private static bool ValidetaTypePurchase(string type)
		{
			if (type == "Retail" || type == "Digital")
			{
				return true;
			}
			return false;

		}
	}
}