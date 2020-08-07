using P01_StudentSystem.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_StudentSystem.Data.Models
{
   public class Resource
    {
        [Key]
        public int ResourceId { get; set; }

        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
        public Course Course { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        public string Url { get; set; }

        public ResourceType ResourceType{ get; set; }
    }
}
