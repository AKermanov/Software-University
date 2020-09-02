using ProductShop.Data;
using ProductShop.Dtos.Import;
using ProductShop.Models;
using ProductShop.XMLHelper;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            ProductShopContext productShopContext = new ProductShopContext();

            //CreateAndDeleteDb(productShopContext);

            ProductShopContext context = productShopContext;

            //p1
            //var usersXml = File.ReadAllText("../../../Datasets/users.xml");
            //var userXml = ImportProducts(context, usersXml);

            //p2
            // var productsXml = File.ReadAllText("../../../Datasets/products.xml");
            // var result = ImportProducts(context, productsXml);

            //p3
            //var categoriesXml = File.ReadAllText("../../../Datasets/categories.xml");
            //var result = ImportProducts(context, categoriesXml);

            var cpXml = File.ReadAllText("../../../Datasets/categories-products.xml");
            var result = ImportCategoryProducts(context, cpXml);
            System.Console.WriteLine(result);

        }

        public static void CreateAndDeleteDb(ProductShopContext db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }

        //01. Import Users
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            const string roodElement = "Users";
            var usersResult = XmlConverter.Deserializer<ImportUserDto>(inputXml, roodElement);

            /*
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
            */

            var users = usersResult
                .Select(u => new User
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age
                })
                .ToArray();

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Length}";
        }

        //02. Import Products
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            const string roodElement = "Products";

            var productDtos = XmlConverter.Deserializer<ImprotProductDto>(inputXml, roodElement);

            var products = productDtos
                .Select(p => new Product
                {
                    Name = p.Name,
                    Price = p.Price,
                    BuyerId = p.BuyerId,
                    SellerId = p.SellerId
                })
                .ToArray();

            context.Products.AddRange(products);

            context.SaveChanges();

            return $"Successfully imported {products.Length}";
        }

        //03. Import Categories
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            const string rootElement = "Categories";

            var categoriesDtio = XmlConverter.Deserializer<ImportCategoryDto>(inputXml, rootElement);

            var categories = categoriesDtio
                .Where(c => c.Name != null)
                .Select(p => new Category
                {
                    Name = p.Name
                })
                .ToArray();
            /*
            List<Category> categories = new List<Category>();

            foreach (var dto in categoriesDtio)
            {
                if (dto.Name == null)
                {
                    continue;
                }

                var category = new Category
                {
                    Name = dto.Name
                };

                categories.Add(category);
            }
            */

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Length}";
        }

        //04. Import Categories and Products
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            var rootElement = "CategoryProducts";

            var categoryProductDtos = XmlConverter.Deserializer<ImportCategoryProductDto>(inputXml, rootElement);

           
            var categories = categoryProductDtos
                .Where(i =>
                context.Categories.Any(s => s.Id == i.CategoryId) &&
                context.Products.Any(s => s.Id == i.ProductId))
                .Select(c => new CategoryProduct
                {
                    CategoryId = c.CategoryId,
                    ProductId = c.ProductId
                })
                .ToArray();
            
            /*
            var categories = new List<CategoryProduct>();

            foreach (var categoryProductDto in categoryProductDtos)
            {
                var doesExist = context.Products.Any(x => x.Id == categoryProductDto.ProdutId) &&
                    context.Categories.Any(x => x.Id == categoryProductDto.CategoryId);

                if (!doesExist)
                {
                    continue;
                }

                var categoryProduct = new CategoryProduct
                {
                    CategoryId = categoryProductDto.CategoryId,
                    ProductId = categoryProductDto.ProdutId
                };

                categories.Add(categoryProduct);
            }
            */

            context.CategoryProducts.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Length}"; ;
        }
    }
}