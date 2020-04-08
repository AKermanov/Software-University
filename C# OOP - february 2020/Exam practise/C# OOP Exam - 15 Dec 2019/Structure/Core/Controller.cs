using AquaShop.Core.Contracts;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Aquariums.Models;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Decorations.Models;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Models.Fish.Models;
using AquaShop.Repositories;
using AquaShop.Repositories.Contracts;
using AquaShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Core
{
    class Controller : IController
    {
        private IRepository<IDecoration> decorations;
        private ICollection<IAquarium> aquariums;

        public Controller()
        {
            this.decorations = new DecorationRepository();
            this.aquariums = new List<IAquarium>();
        }
        public string AddAquarium(string aquariumType, string aquariumName)
        {
            IAquarium aquarium = null;
            if (aquariumType == "FreshwaterAquarium")
            {
                aquarium = new FreshwaterAquarium(aquariumName);
            }
            else if ( aquariumType =="SaltwaterAquarium")
            {
                aquarium = new SaltwaterAquarium(aquariumName);
            }
            else
            {
                return string.Format(ExceptionMessages.InvalidAquariumType);
            }

            aquariums.Add(aquarium);
            return string.Format(OutputMessages.SuccessfullyAdded, aquariumType);
        }
      

        public string AddDecoration(string decorationType)
        {
            IDecoration decoration = null;
            if (decorationType == "Ornament")
            {
                decoration = new Ornament();
            }
            else if (decorationType == "Plant")
            {
                decoration = new Plant();
            }
            else
            {
                return string.Format(ExceptionMessages.InvalidDecorationType);
            }

            decorations.Add(decoration);
            return string.Format(OutputMessages.SuccessfullyAdded, decorationType);
        }

        public string AddFish(string aquariumName, string fishType, 
            string fishName, string fishSpecies, decimal price)
        {
            IFish fish = null;
            if (fishType == nameof(FreshwaterFish))
            {
                fish = new FreshwaterFish(fishName, fishSpecies, price);
            }
            else if (fishType == nameof(SaltwaterFish))
            {
                fish = new SaltwaterFish(fishName, fishSpecies, price);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidFishType);
            }

            var currentAquariumName = aquariums.FirstOrDefault(x => x.Name == aquariumName);

            //if (fish.GetType() == typeof(FreshwaterFish) 
            //    && aquariumName.GetType() == typeof(FreshwaterAquarium))
            //{
            //    currentAquariumName.AddFish(fish);
            //}

            //if (fish.GetType() == typeof(SaltwaterFish) 
            //    && aquariumName.GetType() == typeof(SaltwoterAquarium))
            //{
            //    currentAquariumName.AddFish(fish);
            //}

            char fishChar = fishType[0];
            char aquaName = currentAquariumName.GetType().Name[0];
            if (fishChar != aquaName)
            {
                return string.Format(OutputMessages.UnsuitableWater);
            }
            currentAquariumName.AddFish(fish);
            return string.Format(OutputMessages.EntityAddedToAquarium, fishType, aquariumName);
        }

        public string CalculateValue(string aquariumName)
        {
            var currentAquariumName = aquariums.FirstOrDefault(x => x.Name == aquariumName);

            //var totPrice = currentAquariumName.Fish.Sum(p => p.Price) 
            //+ currentAquariumName.Decorations.Sum(p => p.Price);
            decimal totSum = 0;
            foreach (var item in currentAquariumName.Decorations)
            {
                totSum += item.Price;
            }

            foreach (var item in currentAquariumName.Fish)
            {
                totSum += item.Price;
            }
            var sum = $"{totSum:f2}";
            return string.Format(OutputMessages.AquariumValue, aquariumName, sum);
        }

        public string FeedFish(string aquariumName)
        {
            var count = 0;
            var currentAquariumName = aquariums.FirstOrDefault(x => x.Name == aquariumName);
            foreach (var item in currentAquariumName.Fish)
            {
                item.Eat();
                count++;
            }

            return string.Format(OutputMessages.FishFed, count);
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            var currentDecoration = decorations.FindByType(decorationType);
            var aqaurium = aquariums.FirstOrDefault(x => x.Name == aquariumName);
            if (currentDecoration ==null)
            {
                var message = string.Format(ExceptionMessages.InexistentDecoration, decorationType);
                throw new InvalidOperationException(message);
            }

            decorations.Remove(currentDecoration);
            aqaurium.AddDecoration(currentDecoration);
            return string.Format(OutputMessages.EntityAddedToAquarium, decorationType, aquariumName);
        }

        public string Report()
        {
            var sb = new StringBuilder();
            foreach (var item in aquariums)
            {
                sb.AppendLine(item.GetInfo());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
