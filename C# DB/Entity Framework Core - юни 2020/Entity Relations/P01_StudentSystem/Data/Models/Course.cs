using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace P01_StudentSystem.Data.Models
{
    public class Course
    {
        public Course()
        {
            this.Resources = new HashSet<Resource>();
            this.HomeworkSubmissions = new HashSet<Homework>();
            this.StudentsEnrolled = new HashSet<StudentCourse>();
        }

        [Key]
        public int CourseId { get; set; }

        [StringLength(80)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public decimal Price { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }
        public virtual ICollection<Homework> HomeworkSubmissions { get; set; }
        public virtual ICollection<StudentCourse> StudentsEnrolled { get; set; }
    }
}
