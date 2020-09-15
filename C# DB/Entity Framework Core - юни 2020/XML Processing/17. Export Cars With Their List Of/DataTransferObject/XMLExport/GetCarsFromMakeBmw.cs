using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.DataTransferObject.XMLExport
{
    [XmlType("car")]
   public class GetCarsFromMakeBmw
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("travelled-distance")]
        public long Distance { get; set; }
    }
}
