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

            string input = Console.ReadLine();
            //int year = int.Parse(input);

            //string result = GetBooksByAgeRestriction(db, input);
            //string result = GetGoldenBooks(db);
            //string result = GetBooksNotReleasedIn(db, year);
            //string result = GetBooksByCategory(db, input);
            string result = GetBookTitlesContaining(db, input);


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

        //4. Not Released In
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            List<string> bookNotRealeasedIn = context
                .Books
                //тук datetime може да е null, и за да хвърли ексепшън го взимаме така
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToList();

            return String.Join(Environment.NewLine, bookNotRealeasedIn);
        }

        //5. Book Titles by Category
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            //56:00 обяснение

            //взимаме всички категории, и ги правим case insensitive
            string[] categories = input
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.ToLower())
                .ToArray();

            List<string> bookTitles = new List<string>();
            
            foreach (var cat in categories)
            {
                //за всяка категория от контескта
                List<string> currCatBookTitle = context
                    .Books
                    //взимаме само тези книги, в които mapping context има двойка където името = на моята категория
                    .Where(b => b.BookCategories.Any(bc => bc.Category.Name.ToLower() == cat))
                    .Select(b => b.Title)
                    .ToList();

                bookTitles.AddRange(currCatBookTitle);
            }

            bookTitles = bookTitles
                .OrderBy(bt => bt)
                .ToList();


            return String.Join(Environment.NewLine, bookTitles);
        }

        //8. Book Search
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            //1:06:41 обяснение

            List<string> bookTItlesContainingString = context
                .Books
               // .AsEnumerable() //Because of the exception
                .Select(b => b.Title)
                //.Where(b => b.Title.Contains(input, StringComparison.CurrentCultureIgnoreCase))
                .Where(t=>t.ToLower().Contains(input.ToLower()))
                .OrderBy(bt => bt)
                .ToList();

            return String.Join(Environment.NewLine, bookTItlesContainingString);
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
