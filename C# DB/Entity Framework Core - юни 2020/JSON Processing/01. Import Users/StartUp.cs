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

            string inputJson = File.ReadAllText("../../../Datasets/users.json");

           string result = ImportUsers(db, inputJson);
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
    }
}