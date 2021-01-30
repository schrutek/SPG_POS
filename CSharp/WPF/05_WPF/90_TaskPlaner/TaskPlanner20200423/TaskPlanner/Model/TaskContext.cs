using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace TaskPlanner.Model
{
    public class TaskContext : DbContext
    {
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Task> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=Tasks.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>().HasKey(s => s.Nr);
            modelBuilder.Entity<Subject>().Property(s => s.Nr).HasMaxLength(8);
            modelBuilder.Entity<Subject>().Property(s => s.Name).IsRequired();

            modelBuilder.Entity<Task>().HasOne(t => t.Subject).WithMany(s => s.Tasks).IsRequired();
        }

        public void Seed()
        {
            var subjects = new List<Subject>
            {
                new Subject {Nr = "POS", Name = "Programmieren"},
                new Subject {Nr = "DBI", Name = "Datenbanken"}
            };
            var tasks = new List<Task>
            {
                new Task{ Start = new DateTime(2020, 3, 1), End = new DateTime(2020, 3, 8), Grade = 3, Subject = subjects[0] },
                new Task{ Start = new DateTime(2020, 3, 5), End = new DateTime(2020, 3, 11), Grade = 1, Subject = subjects[0] },
                new Task{ Start = new DateTime(2020, 4, 1), End = new DateTime(2020, 4, 8), Grade = 3, Subject = subjects[1] },
                new Task{ Start = new DateTime(2020, 4, 5), End = new DateTime(2020, 4, 11), Subject = subjects[1] }
            };
            Subjects.AddRange(subjects);
            SaveChanges();
            Tasks.AddRange(tasks);
            SaveChanges();
        }
    }

}
