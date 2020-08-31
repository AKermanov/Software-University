using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.DTO.Product;
using ProductShop.DTO.User;
using ProductShop.Models;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace ProductShop
{
    public class StartUp
    { public const string ResultDirectoryPath = "../../../Datasets/Results";
        //private static readonly IMapper mapper;
        public static void Main(string[] args)
        {
            InitializeMapper();
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
           // string json = GetProductsInRange(db); 

            //p6
            //string json = GetSoldProducts(db); 

            //p7
           // string json = GetCategoriesByProductsCount(db); 

            //p8
            string json = GetUsersWithProducts(db); 

            if (!Directory.Exists(ResultDirectoryPath))
            {
                Directory.CreateDirectory(ResultDirectoryPath);
            }

           //P5 File.WriteAllText(ResultDirectoryPath + "products-in-range.json",json);
          //p6  File.WriteAllText(ResultDirectoryPath + "users-sold-products.json", json);
           //p7 File.WriteAllText(ResultDirectoryPath + "categories-by-products.json", json);
           File.WriteAllText(ResultDirectoryPath + "users-and-products.json", json);

        }

        private static void InitializeMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });
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
                    price = p.Price.ToString("f2"),
                    seller = p.Seller.FirstName + " " + p.Seller.LastName

                })
                .ToArray();

            string json = JsonConvert.SerializeObject(products, Formatting.Indented);

            return json;
        }

        //solve with DTO and automapper 1:27:14
        /*
       public static string GetProductsInRangeWithDto(ProductShopContext context)
       {

           ListProductsInRange[] products = context
               .Products
               .Where(p => p.Price >= 500 && p.Price <= 1000)
               .OrderBy(p => p.Price)
               .Select(p => new ListProductsInRange
               {
                   Name = p.Name,
                   Price = p.Price,
                   SellerName = p.Seller.FirstName + " " + p.Seller.LastName

               })
               .ToArray();


      //automapper
      ListProductsInRange[] products = context
         .Products
         .Where(p => p.Price >= 500 && p.Price <= 1000)
         .OrderBy(p => p.Price)
         .ProjectTo<ListProductsInRange>()
         .ToArray();
         

        string json = JsonConvert.SerializeObject(products, Formatting.Indented);

            return json;
        }
          */

        //06. Export Sold Products 1:38:40
        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context
                .Users
                .Where(u => u.ProductsSold.Any(p => p.Buyer != null))
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    soldProducts = u.ProductsSold
                                    .Where(p => p.Buyer != null)
                                    .Select(p => new
                                    {
                                        name = p.Name,
                                        price = p.Price,
                                        buyerFirstName = p.Buyer.FirstName,
                                        buyerLastName = p.Buyer.LastName
                                    }).ToArray()
                }).ToArray();

            string json = JsonConvert.SerializeObject(users, Formatting.Indented);

            return json;
        }

        //solve with DTO and automapper 1:44:56
        public static string GetSoldProductsAutomap(ProductShopContext context)
        {
            var users = context
                .Users
                .Where(u => u.ProductsSold.Any(p => p.Buyer != null))
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .ProjectTo<UserSoldProductDTO>()
                .ToArray();

            string json = JsonConvert.SerializeObject(users, Formatting.Indented);

            return json;
        }

        //07. Export Categories By Products Count 2:05:26
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context
                .Categories
                .Select(c => new
                {
                    category = c.Name,
                    productsCount = c.CategoryProducts.Count(),
                    averagePrice = c.CategoryProducts.Average(cp => cp.Product.Price).ToString("f2"),
                    totalRevenue = c.CategoryProducts.Sum(cp => cp.Product.Price).ToString("f2")

                })
                .OrderByDescending(c => c.productsCount)
                .ToArray();

            string json = JsonConvert.SerializeObject(categories, Formatting.Indented);

            return json;
        }

        //08. Export Users and Products 2:12:29
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            //2:24:19
            var users = context
                .Users
                .Where(u => u.ProductsSold.Any(p => p.Buyer != null))
                .Select(u=> new 
                { 
                   lastName = u.LastName,
                   age = u.Age,
                   soldProducts = new
                   {
                       count = u.ProductsSold.Count(p=>p.Buyer != null),
                       products = u.ProductsSold
                                   .Where(p=>p.Buyer != null)
                                   .Select(p=> new
                                   {
                                       name = p.Name,
                                       price = p.Price
                                   }).ToArray()
                      
                   }
                })
                .OrderByDescending(u=>u.soldProducts.count)
                .ToArray();

            var resultObj = new
            {
                usersCount = users.Length,
                user = users
            };

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };

            string json = JsonConvert.SerializeObject(resultObj, settings);

            return json;
        }

        //P1: Deserializae with DTO 2:31:00
        //по-сложно е така, усложняваме живота...
        public static string ImportUsersDTO(ProductShopContext context, string inputJson)
        {
            UserImprotDTO[] UsersDto = JsonConvert.DeserializeObject<UserImprotDTO[]>(inputJson);
            //JsonSerializerSettings тук пипаме ако нещо не се е намачнало както трябва

            User[] users = UsersDto
                .Select(udto => Mapper.Map<User>(udto)).ToArray();

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Length}";
        }
    }
}