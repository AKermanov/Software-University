using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using CarDealer.Data;
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

           // ResetDatabase(db);

            //p9
            //string inputJson = File.ReadAllText("../../../Datasets/suppliers.json");
           // string result = ImportSuppliers(db, inputJson);


            //p10
            string inputJson = File.ReadAllText("../../../Datasets/parts.json");
           string result = ImportParts(db, inputJson);


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
               

            List<int> supplier = context.Suppliers
                .Select(s => s.Id)
                .ToList();

            List<Part> validEntities = parts
                .Where(p => supplier.Contains(p.SupplierId))
                .ToList();

            context.Parts.AddRange(validEntities);
            context.SaveChanges();

            return $"Successfully imported {validEntities.Count}.";
        }

    }
}