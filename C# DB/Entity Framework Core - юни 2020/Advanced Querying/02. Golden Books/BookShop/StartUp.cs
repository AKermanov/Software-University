namespace BookShop
{
    using Data;
    using Initializer;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            //DbInitializer.ResetDatabase(db);

            //string input = Console.ReadLine();

            //string result = GetBooksByAgeRestriction(db, input);
            string result = GetGoldenBooks(db);

            Console.WriteLine(result);
        }

        //1. Age Restriction
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            //трябва да материализираме данните, преди да правим каквито и да е било филтрации
            List<string> booksTitles = context
                .Books
                .AsEnumerable()
                .Where(b => b.AgeRestriction.ToString().ToLower() == command.ToLower())
                //когато кажем на на една енумерация toString, връща ни стринга зад числото в енумерацията
                .Select(b => b.Title)
                .OrderBy(bt => bt)
                .ToList();

            return String.Join(Environment.NewLine, booksTitles);
        }

        // 2. Golden Books
        public static string GetGoldenBooks(BookShopContext context)
        {
            List<string> bookTitles = context
                .Books
                .Where(b => b.EditionType == Models.Enums.EditionType.Gold && b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToList();

            return String.Join(Environment.NewLine, bookTitles);
        }
        /*14. Increase Prices
        public static void IncreasePrices(BookShopContext context)
        {
            var books = context
                            .Books
                            .Where(b => b.ReleaseDate.Value.Year < 2010);
            foreach (var b in books)
            {
                b.Price += 5;
            }

            context.SaveChanges();
        }
        */
    }

}
