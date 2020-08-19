namespace BookShop
{
    using Data;
    using Initializer;
    using Microsoft.VisualBasic;
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
            //int year = int.Parse(input);

            //string result = GetBooksByAgeRestriction(db, input);
            //string result = GetGoldenBooks(db);
            //string result = GetBooksNotReleasedIn(db, year);
            //string result = GetBooksByCategory(db, input);
            //string result = GetBookTitlesContaining(db, input);
           // string result = CountCopiesByAuthor(db);
           // string result = GetTotalProfitByCategory(db);
            string result = GetMostRecentBooks(db);


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

        //11. Total Book Copies
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            // 1:20:00 обяснение
            //google ViewModels
            var sb = new StringBuilder();
            var authorCopies = context
                .Authors
                .Select(a => new
                {
                    FullName = a.FirstName + " " + a.LastName,
                    BookCopies = a.Books.Sum(b => b.Copies)
                   // BookCopies = a.Books.Select(b=>b.Copies).Sum()


                })
                .OrderByDescending(a => a.BookCopies)
                .ToList();

            foreach (var a in authorCopies)
            {
                sb
                    .AppendLine($"{a.FullName} - {a.BookCopies}");
            }

            return sb.ToString().TrimEnd();
        }

        //12. Profit by Category
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            //1:29:40 Обяснение
            //1:34:16 Обяснение
            var sb = new StringBuilder();

            var categoryProfits = context
                .Categories
                .Select(c => new
                {
                    c.Name,
                    TotalProfit = c.CategoryBooks
                                    .Select(cb => new
                                    {
                                        BookProfit = cb.Book.Copies * cb.Book.Price
                                    })
                                    .Sum(cb => cb.BookProfit)
                })
                .OrderByDescending(c => c.TotalProfit)
                .ThenBy(c => c.Name)
                .ToList();

            foreach (var c in categoryProfits)
            {
                sb
                    .AppendLine($"{c.Name} ${c.TotalProfit:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        //13. Most Recent Books
        public static string GetMostRecentBooks(BookShopContext context)
        {
            //1:39:25 обяснение
            //1:40:45 обяснение 
            var sb = new StringBuilder();

            var categoriesWithMostRecentBooks = context
                .Categories
                .Select(cb => new
                {
                    CategoryName = cb.Name,
                    MostRecentBooks = cb.CategoryBooks
                        .OrderByDescending(cb => cb.Book.ReleaseDate)
                        .Take(3)
                        .Select(cb => new
                        {
                            BookTitle = cb.Book.Title,
                            RealeseYear = cb.Book
                                   .ReleaseDate.Value.Year
                        })
                        .ToList()
                })
                .OrderBy(c => c.CategoryName)
                .ToList();

            foreach (var c in categoriesWithMostRecentBooks)
            {
                sb
                    .AppendLine($"--{c.CategoryName}");
                foreach (var b in c.MostRecentBooks)
                {
                    sb.AppendLine($"{b.BookTitle} ({b.RealeseYear})");
                }
            }

            return sb.ToString().TrimEnd();
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
