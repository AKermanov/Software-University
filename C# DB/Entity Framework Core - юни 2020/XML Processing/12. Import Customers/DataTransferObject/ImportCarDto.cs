using System.Xml.Serialization;

namespace CarDealer.DataTransferObject
{
    //всичко което трябва да запомним за изпита е в този клас
    [XmlType("Car")]
   public class ImportCarDto
    {
        [XmlElement("make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("TraveledDistance")]
        public long TraveledDistance { get; set; }

        [XmlElement("parts")]
        public ImportCarPartDto[] Parts { get; set; }
    }

    [XmlType("partId")]
    public class ImportCarPartDto
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}