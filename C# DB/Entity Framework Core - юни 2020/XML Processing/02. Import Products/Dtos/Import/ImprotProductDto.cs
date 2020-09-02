using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ProductShop.Dtos.Import
{
    [XmlType("Product")]
   public class ImprotProductDto
    {
        public string Name { get; set; }


        public decimal Price { get; set; }


        public int SellerId { get; set; }

        public int BuyerId { get; set; }
    }
}
