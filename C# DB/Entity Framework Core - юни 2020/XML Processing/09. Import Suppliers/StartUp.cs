using CarDealer.Data;
using CarDealer.DataTransferObject;
using CarDealer.Models;
using CarDealer.XMLHelper;
using System.Linq;
using System.Xml;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {

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
    }
}