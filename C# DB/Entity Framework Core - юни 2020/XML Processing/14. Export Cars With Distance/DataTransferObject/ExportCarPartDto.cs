using System.Xml.Serialization;

namespace CarDealer.DataTransferObject
{
    [XmlType("part")]
   public class ExportCarPartDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("price")]
        public decimal Price { get; set; }
    }
}
