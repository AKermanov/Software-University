using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.DataTransferObject.XMLExport
{
    using System.Xml.Serialization;

    [XmlType("customer")]
    public class TotalSaleByCustomerDto
    {
        [XmlAttribute("full-name")]
        public string FullName { get; set; }

        [XmlAttribute("bought-cars")]
        public int BoughtCars { get; set; }

        [XmlAttribute("spent-money")]
        public decimal SpentMoney { get; set; }
    }
}
