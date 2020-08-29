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
    { public const string ResultDirectoryPath = "../../../Datasets/Results";
        //private static readonly IMapper mapper;
        public static void Main(string[] args)
        {
            ProductShopContext db = new ProductShopContext();
            //Deserialisation P1-P4
            //P1  string inputJson = File.ReadAllText("../../../Datasets/users.json");
            //P2  string inputJson = File.ReadAllText("../../../Datasets/products.json");
            //P3 string inputJson = File.ReadAllText("../../../Datasets/categories.json");
            //P4 string inputJson = File.ReadAllText("../../../Datasets/categories-products.json");


            //string result = ImportCategoryProducts(db, inputJson);
            //Console.WriteLine(result);

            // ResetDatabase(db);



            //Serealisation
            //P5
            string json = GetProductsInRange(db);

            if (!Directory.Exists(ResultDirectoryPath))
            {
                Directory.CreateDirectory(ResultDirectoryPath);
            }

            File.WriteAllText(ResultDirectoryPath + "products-in-range.json",json);

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
                .Where(c => c.Name != null)
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

        //04. Import Categories and Products 1:07:13
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            CategoryProduct[] categoryProducts = JsonConvert
                .DeserializeObject<CategoryProduct[]>(inputJson);

            context.CategoryProducts.AddRange(categoryProducts);

            //за web project е по добре да се позлава context.SaveChangesAsync()

            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Length}";
        }

        //05. Export Products In Range 1:16:35
        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context
                .Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    seller = p.Seller.FirstName + " " + p.Seller.LastName

                })
                .ToArray();

            string json = JsonConvert.SerializeObject(products, Formatting.Indented);

            return json;
        }
    }
}