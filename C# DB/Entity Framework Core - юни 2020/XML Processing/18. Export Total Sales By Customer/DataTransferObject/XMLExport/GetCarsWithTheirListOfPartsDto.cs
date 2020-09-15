namespace CarDealer.Dtos.Export
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType("car")]
    public class GetCarsWithTheirListOfPartsDto
    {
        [XmlAttribute("make")]
        public string Make { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("travelled-distance")]
        public long TravelledDistance { get; set; }

        [XmlArray(ElementName = "parts")]
        public List<ExportPartDto1> Parts { get; set; }
    }

   
        [XmlType("part")]
        public class ExportPartDto1
        {
            [XmlAttribute("name")]
            public string Name { get; set; }

            [XmlAttribute("price")]
            public decimal Price { get; set; }
        }
    
}