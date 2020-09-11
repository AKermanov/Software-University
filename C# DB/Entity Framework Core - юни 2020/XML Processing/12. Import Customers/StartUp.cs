using CarDealer.Data;
using CarDealer.DataTransferObject;
using CarDealer.Models;
using CarDealer.XMLHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            CarDealerContext carDealerContext = new CarDealerContext();

            ImportSuppliers(carDealerContext, File.ReadAllText("../../../Datasets/suppliers.xml"));
            ImportParts(carDealerContext, File.ReadAllText("../../../Datasets/parts.xml"));
            ImportCars(carDealerContext, File.ReadAllText("../../../Datasets/cars.xml"));
            ImportCustomers(carDealerContext, File.ReadAllText("../../../Datasets/customers.xml"));

        }


        //09. Import Suppliers
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var suppliersDtos = XmlConverter.Deserializer<ImportSupplierDto>(inputXml, "Suppliers");

            var result = suppliersDtos.Select(s => new Supplier
            {
                Name = s.Name,
                IsImporter = s.IsImported

            })
            .ToArray();

            context.Suppliers.AddRange(result);
            context.SaveChanges();

            return $"Successfully imported {suppliersDtos.Length}";
        }


        //10. Import Parts
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            var partsDtops = XmlConverter.Deserializer<ImportPartsDto>(inputXml, "Parts");

            var parts = partsDtops
                .Where(i => context.Suppliers.Any(s => s.Id == i.SupplierId))
                .Select(x => new Part
                {
                    Name = x.Name,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    SupplierId = x.SupplierId
                })
                .ToArray();

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Length}";
        }

        //11. Import Cars
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            //2:39:36, 2:49:12
            var carsDtos = XmlConverter.Deserializer<ImportCarDto>(inputXml, "Cars");

            var cars = new List<Car>();

            foreach (var carDto in carsDtos)
            {
                var uniqueParts = carDto.Parts.Select(x => x.Id).Distinct().ToArray();
                var realPartds = uniqueParts.Where(id => context.Parts.Any(i => i.Id == id));

                var car = new Car
                {
                    Make = carDto.Make,
                    Model = carDto.Model,
                    TravelledDistance = carDto.TraveledDistance,
                    PartCars = realPartds.Select(id => new PartCar
                    {
                        PartId = id
                    })
                    .ToArray()
                };

                cars.Add(car);
            }
            context.Cars.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count}";
        }


        //12. Import Customers
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            var customersDtos = XmlConverter.Deserializer<ImportCustomerDto>(inputXml, "Customers");

            var customers = customersDtos.Select(x => new Customer
            {
                Name = x.Name,
                IsYoungDriver = x.IsYoungDriver,
                BirthDate = DateTime.Parse(x.BirthDate)
            })
            .ToArray();

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Length}";
        }
    }
}