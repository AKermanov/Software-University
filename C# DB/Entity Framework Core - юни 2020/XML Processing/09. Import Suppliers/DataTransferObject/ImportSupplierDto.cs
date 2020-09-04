using System.Xml.Serialization;

namespace CarDealer.DataTransferObject
{
    [XmlType("Supplier")]
   public class ImportSupplierDto
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("isImported")]
        public bool IsImported { get; set; }
    }
}
