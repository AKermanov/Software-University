
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaporStore.Data.Models
{
   public class Card
    {
        public Card()
        {
            this.Purchases = new HashSet<Purchase>();
        }
        [Key]
        public int Id { get; set; }

        [MaxLength(19)]
        public string Number { get; set; }

        [Required]
        [MaxLength(3)]
        //validation requred
        public string Cvc { get; set; }

        //add validation 0-1
        [Required]
        public CardType Type { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }

        public virtual ICollection<Purchase> Purchases  { get; set; }
    }
}
