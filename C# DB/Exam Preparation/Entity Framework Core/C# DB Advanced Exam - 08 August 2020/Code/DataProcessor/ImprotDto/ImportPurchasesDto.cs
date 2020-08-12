using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace VaporStore.DataProcessor.ImprotDto
{
    [XmlType("Purchase")]
   public class ImportPurchasesDto
    {
        [XmlAttribute("title")]
        [Required]
        public string Title { get; set; }

       
        [XmlElement("Type")]
        [Required]
        public string Type { get; set; }

    
        [RegularExpression(@"^[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}$")]
        [XmlElement("Key")]
        [Required]
        public string ProductKey { get; set; }

        
        [XmlElement("Card")]
        [RegularExpression(@"^[0-9]{4} [0-9]{4} [0-9]{4} [0-9]{4}$")]
        [Required]
        public string CardNumber { get; set; }


        
        [XmlElement("Date")]
        [Required]
        public string Date { get; set; }
    }
}

/*
•	Type – enumeration of type PurchaseType, with possible values (“Retail”, “Digital”) (required) 
•	ProductKey – text, which consists of 3 pairs of 4 uppercase Latin letters and digits, separated by dashes (ex. “ABCD-EFGH-1J3L”) (required)
•	Date – Date (required)
•	Card – the purchase’s card (required)
•	Game – the purchase’s game (required)

 */
 