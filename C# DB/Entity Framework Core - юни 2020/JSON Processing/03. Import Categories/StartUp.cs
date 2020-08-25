using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        //private static readonly IMapper mapper;
        public static void Main(string[] args)
        {
            ProductShopContext db = new ProductShopContext();

            //P1  string inputJson = File.ReadAllText("../../../Datasets/users.json");
            //P2  string inputJson = File.ReadAllText("../../../Datasets/products.json");
           string inputJson = File.ReadAllText("../../../Datasets/categories.json");

            string result = ImportCategories(db, inputJson);
            Console.WriteLine(result);
        }

        private static void ResetDatabase(ProductShopContext db)
        {
            db.Database.EnsureDeleted();
            Console.WriteLine("Database was successfully deleted!");
            db.Database.EnsureCreated();
            Console.WriteLine("Databse was seccessfully created!");
        }


        //01. Import Users 34:40
        //43:53
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            User[] users = JsonConvert.DeserializeObject<User[]>(inputJson);
            //JsonSerializerSettings тук пипаме ако нещо не се е намачнало както трябва
            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Length}";
        }

        //02. Import Products 51:12
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            Product[] products = JsonConvert.DeserializeObject<Product[]>(inputJson);

            context.Products.AddRange(products);
            context.SaveChanges();

           return $"Successfully imported {products.Length}";
        }

        //03. Import Categories
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
              //вариянт 1
            Category[] categories = JsonConvert
                .DeserializeObject<Category[]>(inputJson)
                .Where(c=>c.Name != null)
                .ToArray();
           
            context.Categories.AddRange(categories);
             

            /*
             //вариянт 2
            foreach (Category category in categories)
            {
                if (category.Name != null)
                {
                    context.Categories.Add(category);
                }
            }
            */


            /*
            //вариянт 3 1:03:22 не работи само за пропъртито, но НЕ игнорира целия обект!!!
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            Category[] categories = JsonConvert
               .DeserializeObject<Category[]>(inputJson,settings);
              
            context.Categories.AddRange(categories);
            */


            context.SaveChanges();

            return $"Successfully imported {categories.Length}";
        }
    }
}