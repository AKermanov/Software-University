using ProductShop.Data;
using ProductShop.Dtos.Export;
using ProductShop.Dtos.Import;
using ProductShop.Models;
using ProductShop.XMLHelper;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

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

            //p4
            //var cpXml = File.ReadAllText("../../../Datasets/categories-products.xml");
            //var result = ImportCategoryProducts(context, cpXml);

            //p5
         //   var productInRange = GetProductsInRange(context);
           // File.WriteAllText("../../../Result/productInRange.xml", productInRange);

            //p8
            var productInRange = GetUsersWithProducts(context);
            File.WriteAllText("../../../Results/result.xml", productInRange);

            // System.Console.WriteLine(result);

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
            var usersResult = XMLConverter.Deserializer<ImportUserDto>(inputXml, roodElement);

            /* Първи начин
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

            //Втори начин
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

            var productDtos = XMLConverter.Deserializer<ImprotProductDto>(inputXml, roodElement);

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

            var categoriesDtio = XMLConverter.Deserializer<ImportCategoryDto>(inputXml, rootElement);

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

            var categoryProductDtos = XMLConverter.Deserializer<ImportCategoryProductDto>(inputXml, rootElement);


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

        //05. Export Products In Range
        public static string GetProductsInRange(ProductShopContext context)
        {
            const string roodElement = "Products";

            var products = context
                .Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(x => new ExportProductDto
                {
                    Name = x.Name,
                    Price = x.Price,
                    Buyer = x.Buyer.FirstName + " " + x.Buyer.LastName
                })
                .OrderBy(p => p.Price) //възходящ ред
                .Take(10)
                .ToList();

            var result = XMLConverter.Serialize(products, roodElement);

            return result;
        }

        // 06. Export Sold Products
        public static string GetSoldProducts(ProductShopContext context)
        {
            var rootElement = "Users";

            var usersWithProducts =
                context.Users
                .Where(u => u.ProductsSold.Any())
                .Select(x => new ExprotSoldProductDto1
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    SoldProducts = x.ProductsSold
                        .Select(p => new UserProductDto
                        {
                            Name = p.Name,
                            Price = p.Price
                        })
                        .ToArray()
                })
                .OrderBy(l => l.LastName)
                .ThenBy(f => f.FirstName)
                .Take(5)
                .ToArray();

            var result = XMLConverter.Serialize(usersWithProducts, rootElement);

            return result;
        }

        //07. Export Categories By Products Count
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var rootElement = "Categories";

            var categories = context.Categories
                .Select(c => new ExportCategoryDto
                {
                    Name = c.Name,
                    Count = c.CategoryProducts.Count(),
                    TotalRevenue = c.CategoryProducts
                                    .Sum(p => p.Product.Price),
                    AveragePrice = c.CategoryProducts
                                    .Average(p => p.Product.Price),
                })
                .OrderByDescending(c => c.Count)
                .ThenBy(t => t.TotalRevenue)
                .ToArray();

            var result = XMLConverter.Serialize(categories, rootElement);

            return result;
        }

        //08. Export Users and Products
      /*  public static string GetUsersWithProducts(ProductShopContext context)
        {
            //1:51:14
            var usersAndProducts = context.Users
                .ToArray()
                .Where(p => p.ProductsSold.Any())
                .Select(u => new ExportUserDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    SoldProduct = new ExportProductCountDto
                    {
                        Count = u.ProductsSold.Count,
                        Products = u.ProductsSold.Select(p => new ExportProducDto
                        {
                            Name = p.Name,
                            Price = p.Price
                        })
                        .OrderByDescending(p=>p.Price)
                        .ToArray()
                    }
                })
                .OrderByDescending(x=>x.SoldProduct.Count)
                .Take(10)
                .ToArray();


            var resultDto = new ExportUsersCountDto
            {
                Count = context.Users.Count(p=>p.ProductsSold.Any()),
                Users = usersAndProducts
            };

            var result = XmlConverter.Serialize(resultDto, "Users");

            return result;
        }
      */

       public static string GetUsersWithProducts(ProductShopContext context)
        {
            var stringBuilder = new StringBuilder();

            var users = context
                .Users
                .AsEnumerable()
                .Where(u => u.ProductsSold.Any())
                .OrderByDescending(u => u.ProductsSold.Count)
                .Select(u => new ExportUserDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    SoldProducts = new ExportSoldProductDto
                    {
                        Count = u.ProductsSold.Count,
                        Products = u.ProductsSold
                                                 .Select(p => new UserSoldProductDto
                                                 {
                                                     Name = p.Name,
                                                     Price = p.Price
                                                 })
                                                 .OrderByDescending(p => p.Price)
                                                 .ToArray()
                    }
                })
                .Take(10)
                .ToArray();

            var resultUsers = new ExportUserAndProductDto
            {
                Count = context.Users.Count(p => p.ProductsSold.Any()),
                Users = users
            };

            var xmlSerializer = new XmlSerializer(typeof(ExportUserAndProductDto), new XmlRootAttribute("Users"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            xmlSerializer.Serialize(new StringWriter(stringBuilder), resultUsers, namespaces);

            return stringBuilder.ToString().TrimEnd();
        }


    }
}