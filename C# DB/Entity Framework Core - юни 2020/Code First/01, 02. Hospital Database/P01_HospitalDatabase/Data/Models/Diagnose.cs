using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P01_HospitalDatabase.Data.Models
{
   public class Diagnose
    {
        [Key]
        public int DiagnoseId { get; set; }
        public string Name { get; set; }
        public string Comments { get; set; }

        [ForeignKey(nameof(Patient))]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
