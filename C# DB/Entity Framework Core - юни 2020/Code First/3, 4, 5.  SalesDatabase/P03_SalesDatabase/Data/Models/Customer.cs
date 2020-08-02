using P03_SalesDatabase.Common;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace P03_SalesDatabase.Data.Models
{
   public class Customer
    {
        public Customer()
        {
            this.Sales = new HashSet<Sale>();
        }

        [Key]
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(GlobalConstants.CustomerNameMaxLenght)]
        public string Name { get; set; }

        [Required]
        [MaxLength(GlobalConstants.CustomerMailMaxLenght)]
        public string Email { get; set; }

        public string CreditCardNumber { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
