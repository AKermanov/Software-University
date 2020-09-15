using CarDealer.Data;
using CarDealer.DataTransferObject;
using CarDealer.DataTransferObject.XMLExport;
using CarDealer.Dtos.Export;
using CarDealer.Models;
using CarDealer.XMLHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            CarDealerContext carDealerContext = new CarDealerContext();

            carDealerContext.Database.EnsureDeleted();
            carDealerContext.Database.EnsureCreated();

            ImportSuppliers(carDealerContext, File.ReadAllText("../../../Datasets/suppliers.xml"));
            ImportParts(carDealerContext, File.ReadAllText("../../../Datasets/parts.xml"));
            ImportCars(carDealerContext, File.ReadAllText("../../../Datasets/cars.xml"));
            ImportCustomers(carDealerContext, File.ReadAllText("../../../Datasets/customers.xml"));
            ImportSales(carDealerContext, File.ReadAllText("../../../Datasets/sales.xml"));

            var result = GetSalesWithAppliedDiscount(carDealerContext);

            File.WriteAllText("../../../result.xml", result);
        }

        //09. Import Suppliers
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var suppliersDtos = XMLConverter.Deserializer<ImportSupplierDto>(inputXml, "Suppliers");

            var result = suppliersDtos.Select(s => new Supplier
            {
                Name = s.Name,
                IsImporter = s.IsImporter

            })
            .ToArray();

            context.Suppliers.AddRange(result);
            context.SaveChanges();

            return $"Successfully imported {suppliersDtos.Length}";
        }


        //10. Import Parts
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            var partsDtops = XMLConverter.Deserializer<ImportPartsDto>(inputXml, "Parts");

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
            var carsDtos = XMLConverter.Deserializer<ImportCarDto>(inputXml, "Cars");

            var cars = new List<Car>();

            foreach (var carDto in carsDtos)
            {
                //2:49:12
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
            var customersDtos = XMLConverter.Deserializer<ImportCustomerDto>(inputXml, "Customers");

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


        //13. Import Sales
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            //2:57:00
            var selesDtos = XMLConverter.Deserializer<ImportSaleDto>(inputXml, "Sales");

            var seles = selesDtos
                .Where(i => context.Cars.Any(x => x.Id == i.CarId))
                .Select(c => new Sale
                {
                    CarId = c.CarId,
                    CustomerId = c.CustomerId,
                    Discount = c.Discount
                })
                .ToArray();

            context.Sales.AddRange(seles);
            context.SaveChanges();

            return $"Successfully imported {seles.Length}";
        }

        //14. Export Cars With Distance
        public static string GetCarsWithDistance(CarDealerContext context)
        {
            var sb = new StringBuilder();

            var cars = context.Cars
                .Where(x => x.TravelledDistance > 2000000)
                .Select(x => new CarsWithDistanceDto
                {
                    Make = x.Make,
                    Model = x.Model,
                    Distance = x.TravelledDistance
                })
                .OrderBy(x => x.Make)
                .ThenBy(x => x.Model)
                .Take(10)
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(CarsWithDistanceDto[]), new XmlRootAttribute("cars"));
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            xmlSerializer.Serialize(new StringWriter(sb), cars, namespaces);

            return sb.ToString().TrimEnd();

        }


        //15. Export Cars From Make BMW
        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            var result = context
                .Cars
                .Where(x => x.Make == "BMW")
                .Select(x => new GetCarsFromMakeBmw
                {
                    Id = x.Id,
                    Model = x.Model,
                    Distance = x.TravelledDistance
                })
                .OrderBy(x => x.Model)
                .ThenByDescending(x => x.Distance)
                .ToList();

            var xmlResult = XMLConverter.Serialize(result, "cars");

            return xmlResult;
        }

        //16. Export Local Suppliers
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var stringBuilder = new StringBuilder();

            var suppliers = context.Suppliers
                .Where(x => !x.IsImporter)
                .Select(x => new GetLocalSuppliersDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Count = x.Parts.Count
                })
                .ToList();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<GetLocalSuppliersDto>), new XmlRootAttribute("suppliers"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            xmlSerializer.Serialize(new StringWriter(stringBuilder), suppliers, namespaces);

            return stringBuilder.ToString().TrimEnd();

        }


        //17. Export Cars With Their List Of Parts
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var stringBuilder = new StringBuilder();

            var cars = context
                .Cars
                .OrderByDescending(c => c.TravelledDistance)
                .ThenBy(c => c.Model)
                .Select(c => new GetCarsWithTheirListOfPartsDto
                {

                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                    Parts = c.PartCars
                    .Select(x => new ExportPartDto1
                    {
                        Name = x.Part.Name,
                        Price = x.Part.Price
                    })
                    .OrderByDescending(p => p.Price)
                    .ToList()
                })
                .Take(5)
                .ToList();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ExportCarDto>), new XmlRootAttribute("cars"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            xmlSerializer.Serialize(new StringWriter(stringBuilder), cars, namespaces);

            return stringBuilder.ToString().TrimEnd();
        }

        //18. Export Total Sales By Customer
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var stringBuilder = new StringBuilder();

            var customers = context
                .Customers
                .Where(c => c.Sales.Count > 0)
                .Select(c => new TotalSaleByCustomerDto
                {
                    FullName = c.Name,
                    BoughtCars = c.Sales.Count,
                    SpentMoney = c.Sales.Sum(s => s.Car.PartCars.Sum(x => x.Part.Price))
                })
                .OrderByDescending(c => c.SpentMoney)
                .ToList();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<TotalSaleByCustomerDto>), new XmlRootAttribute("customers"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            xmlSerializer.Serialize(new StringWriter(stringBuilder), customers, namespaces);

            return stringBuilder.ToString().TrimEnd();
        }
        //19. Sales with Applied Discount 3:00:00
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            //3:01:15
            var result = context.Sales
                .Select(s => new ExportSaleDto
                {
                    Car = new ExportCarDto
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TraveledDistance = s.Car.TravelledDistance
                    },
                    Discount = s.Discount,
                    CustomerName = s.Customer.Name,
                    Price = s.Car.PartCars.Sum(p => p.Part.Price),
                    PriceWithDiscount = s.Car.PartCars.Sum(p => p.Part.Price) - s.Car.PartCars.Sum(p => p.Part.Price) * s.Discount / 100.00M
                })
                .ToArray();

            var xmlResult = XMLConverter.Serialize(result, "sales");

            return xmlResult;
        }
    }
}