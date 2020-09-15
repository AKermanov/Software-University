using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.DataTransferObject.XMLExport
{
    [XmlType("car")]
   public class GetCarsWithTheirListOfPartsDtoNew
    {
        [XmlAttribute("make")]
        public string Make { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("travelled-distance")]
        public long TravelledDistance { get; set; }

        [XmlArray(ElementName = "parts")]
        public List<ExportPartDtoNew> Parts { get; set; }
    }

    [XmlType("part")]
    public class ExportPartDtoNew
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("price")]
        public decimal Price { get; set; }
    }
}
