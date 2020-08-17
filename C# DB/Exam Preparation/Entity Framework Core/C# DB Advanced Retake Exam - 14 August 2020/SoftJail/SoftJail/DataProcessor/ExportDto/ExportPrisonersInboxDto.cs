using System.Linq;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ExportDto
{
    [XmlType("Prisoner")]
   public class ExportPrisonersInboxDto
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("IncarcerationDate")]
        public string IncarcerationDate { get; set; }

        [XmlArray("EncryptedMessages")]
        public EncryptedMessages[] EncrMessages { get; set; }
    }

    [XmlType("Message")]
    public class EncryptedMessages
    {
        /*
           private string desctiptionReverse;

           [XmlElement("Description")]
           public string DesctiptionReverse
           {
               get { return desctiptionReverse; }
               set { desctiptionReverse = value.Reverse().ToString(); }
           }
        */
        [XmlElement("Description")]
        public string DesctiptionReverse { get; set; }
      
    }
}
