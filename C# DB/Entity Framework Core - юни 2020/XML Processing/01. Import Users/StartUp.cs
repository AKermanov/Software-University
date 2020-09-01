using ProductShop.Data;
using ProductShop.Dtos.Import;
using ProductShop.Models;
using ProductShop.XMLHaper;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            ProductShopContext productShopContext = new ProductShopContext();

            CreateAndDeleteDb(productShopContext);

            ProductShopContext context = productShopContext;
            
               var usersXml = File.ReadAllText("../../../Datasets/users.xml");

                var result = ImportUsers(context, usersXml);

                System.Console.WriteLine(result);
            
        }

        public static void CreateAndDeleteDb(ProductShopContext db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            const string roodElement = "Users";
            var usersResult = XmlConverter.Deserializer<ImportUserDto>(inputXml, roodElement);

            List<User> users = new List<User>();

            foreach (var importUserDtio in usersResult)
            {
                var user = new User
                {
                    FirstName = importUserDtio.FirstName,
                    LastName = importUserDtio.LastName,
                    Age = importUserDtio.Age
                };

                users.Add(user);
            }

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count}";
        }
    }
}