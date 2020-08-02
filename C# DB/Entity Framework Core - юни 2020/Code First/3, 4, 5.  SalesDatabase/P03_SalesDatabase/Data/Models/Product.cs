using P03_SalesDatabase.Common;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P03_SalesDatabase.Data.Models
{
   public class Product
    {
        public Product()
        {
            this.Sales = new HashSet<Sale>();
        }
        [Key]
        public int ProductId { get; set; }

        [Required]
        //[MinLength(10)] - предвиден е за view модели, и не е рейндж в който може да е стринга
        [MaxLength(GlobalConstants.ProductNameMaxLenght)]
        public string Name { get; set; }

        [MaxLength(GlobalConstants.ProductDescriptionMaxLenght)]
        public string Description { get; set; }

        public double Quantity { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
