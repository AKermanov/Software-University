
using System.Xml.Serialization;

namespace CarDealer.DataTransferObject.XMLExport
{
    [XmlType("suplier")]
   public class GetLocalSuppliersDto
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("parts-count")]
        public int Count { get; set; }
    }
}
