using System.Xml.Serialization;

namespace ProductShop.Models
{
    [XmlType("CategoryProduct")]
   public class ImportCategoryProductDto
    {
        [XmlElement("CategoryId")]
        public int CategoryId { get; set; }

        [XmlElement("ProductId")]
        public int ProductId { get; set; }
    }
}
