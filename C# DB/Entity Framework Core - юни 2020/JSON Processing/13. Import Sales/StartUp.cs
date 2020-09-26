using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using CarDealer.Data;
using CarDealer.DTO;
using CarDealer.Models;
using Newtonsoft.Json;

namespace CarDealer
{
    public class StartUp
    {
        private const string ResultDirectoryPath = "../../../Datasets/Results";

        public static void Main()
        {
            CarDealerContext db = new CarDealerContext();

            //ResetDatabase(db);

            //p9
            // string inputJson = File.ReadAllText("../../../Datasets/suppliers.json");
            //string result = ImportSuppliers(db, inputJson);


            //p10
            //string inputJson = File.ReadAllText("../../../Datasets/parts.json");
            //string result = ImportParts(db, inputJson);

            //p11
            // string inputJson = File.ReadAllText("../../../Datasets/cars.json");
            // string result = ImportCars(db, inputJson);

            //p12
            //string inputJson = File.ReadAllText("../../../Datasets/customers.json");
            //string result = ImportCustomers(db, inputJson);

            //p12
            string inputJson = File.ReadAllText("../../../Datasets/sales.json");
            string result = ImportSales(db, inputJson);

            Console.WriteLine(result);
        }

        private static void ResetDatabase(CarDealerContext db)
        {
            db.Database.EnsureDeleted();
            Console.WriteLine("Database was successfully deleted!");

            db.Database.EnsureCreated();
            Console.WriteLine("Databse was seccessfully created!");
        }

        //09. Import Suppliers
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            List<Supplier> suppliers = JsonConvert.DeserializeObject<List<Supplier>>(inputJson);

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count}.";
        }

        //10. Import Parts
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            List<Part> parts = JsonConvert.DeserializeObject<List<Part>>(inputJson);

            //когато имаме missing FK, взимаме id от едната таблица
            List<int> supplier = context.Suppliers
                .Select(s => s.Id)
                .ToList();

            //сравняваме с id при FK
            List<Part> validEntities = parts
                .Where(p => supplier.Contains(p.SupplierId))
                .ToList();

            context.Parts.AddRange(validEntities);
            context.SaveChanges();

            return $"Successfully imported {validEntities.Count}.";
        }

        //11. Import Cars
        // insert entity with collection for many to many relation
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var carDtos = JsonConvert.DeserializeObject<List<CarDto>>(inputJson);

            var cars = new List<Car>();

            var carParts = new List<PartCar>();

            foreach (var carDTO in carDtos)
            {
                var newCar = new Car()
                {
                    Make = carDTO.Make,
                    Model = carDTO.Model,
                    TravelledDistance = carDTO.TravelledDistance
                };

                cars.Add(newCar);

                foreach (var partId in carDTO.PartsId.Distinct())
                {
                    var newPartCar = new PartCar
                    {
                        PartId = partId,
                        Car = newCar
                    };

                    carParts.Add(newPartCar);
                }
            }

            context.Cars.AddRange(cars);

            context.PartCars.AddRange(carParts);

            context.SaveChanges();

            return $"Successfully imported {cars.Count}.";
        }

        //12. Import Customers
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(inputJson);

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count}.";
        }

        //13 Import Sales
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            List<Sale> sales = JsonConvert.DeserializeObject<List<Sale>>(inputJson);

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count}.";
        }
    }
}