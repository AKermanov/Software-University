using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_StudentSystem.Data
{
   public class StudentSystemContext : DbContext
    {
        public StudentSystemContext() { }

        public StudentSystemContext(DbContextOptions options)
            : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Connection.ConnectionString);
            }
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Homework> HomeworkSubmissions { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity
                    .Property(e => e.PhoneNumber)
                    .IsUnicode(false)
                    .IsRequired(false);

                entity
                 .Property(e => e.Birthday)
                 .HasDefaultValueSql("GETDATE()")
                 .IsRequired(false);

                entity
                 .Property(e => e.RegisteredOn)
                 .HasDefaultValueSql("GETDATE()")
                 .IsRequired(true);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity
                     .Property(e => e.Description)
                     .IsRequired(false)
                     .IsUnicode(true);
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity
                    .Property(e => e.Url)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity
                    .Property(e => e.Url)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Homework>(entity =>
            {
                entity
                    .Property(e => e.Content)
                    .IsUnicode(false)
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.StudentId });
            });
        }
    }
}
