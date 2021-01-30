using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskPlaner.Model
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
            modelBuilder.Entity<Subject>().HasKey(c => c.Nr);
            modelBuilder.Entity<Subject>().Property(c => c.Nr).HasMaxLength(8);
            modelBuilder.Entity<Subject>().Property(c => c.Name).IsRequired();

            // Key von Task wird automatisch angeleget weil Name=Id, und zum PK mit AutoIncrement weil int

            modelBuilder.Entity<Task>().HasOne(c => c.Subject).WithMany(c=>c.Tasks).IsRequired();
        }

        public void Seed()
        {
            var subjects = new List<Subject>()
            {
                new Subject() {  Nr = "POS", Name = "Programmieren"},
                new Subject() {  Nr = "DBI", Name = "Datenbanken"}
            };

            var tasks = new List<Task>()
            {
                new Task() { Start = new DateTime(2020, 03, 01), End = new DateTime(2020, 03, 08), Grade = 3, Subject = subjects[0] },
                new Task() { Start = new DateTime(2020, 03, 05), End = new DateTime(2020, 03, 11), Grade = 1, Subject = subjects[0] },

                new Task() { Start = new DateTime(2020, 03, 01), End = new DateTime(2020, 03, 08), Grade = 3, Subject = subjects[1] },
                new Task() { Start = new DateTime(2020, 03, 05), End = new DateTime(2020, 03, 11), Grade = 1, Subject = subjects[1] }
            };
            Subjects.AddRange(subjects);
            SaveChanges();

            Tasks.AddRange(tasks);
            SaveChanges();
        }
    }
}
