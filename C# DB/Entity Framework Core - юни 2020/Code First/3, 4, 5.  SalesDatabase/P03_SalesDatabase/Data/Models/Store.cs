using P03_SalesDatabase.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P03_SalesDatabase.Data.Models
{
   public class Store
    {
        public Store()
        {
            this.Sales = new HashSet<Sale>();
        }
        [Key]
        public int StoreId { get; set; }

        [Required]
        [MaxLength(GlobalConstants.StoreNameMaxLenght)]
        public string Name { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
