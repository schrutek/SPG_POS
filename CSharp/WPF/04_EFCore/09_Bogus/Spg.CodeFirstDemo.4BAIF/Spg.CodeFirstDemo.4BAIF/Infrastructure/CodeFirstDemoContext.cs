using Bogus;
using Microsoft.EntityFrameworkCore;
using Spg.CodeFirstDemo._4BAIF.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.CodeFirstDemo._4BAIF.Infrastructure
{
    public class CodeFirstDemoContext : DbContext
    {
        public DbSet<Pupil> Pupils => Set<Pupil>();

        public void Seed()
        {
            // Deterministischen Zufallsgenerator erstellen
            Randomizer.Seed = new Random(174702);

            //
            // Generieren von Lehrer
            var teachers = new Faker<Teacher>("de")
                .Rules((f, t) =>
                {
                    t.Firstname = f.Name.FirstName();
                    t.Lastname = f.Name.LastName();
                    t.Email = $"{t.Lastname.ToLower()}@spengergasse.at";
                    t.AccountName = t.Lastname.ToLower();
                    if (t.Lastname.Length >= 3)
                    {
                        t.Id = t.Lastname.ToUpper().Substring(0, 3);
                    }
                    else
                    {
                        t.Id = t.Lastname.ToUpper().PadRight(3, 'X');
                    }
                })
                .Generate(200)
                .ToHashSet()
                .ToList();

            Teachers.AddRange(teachers);
            SaveChanges();

            //
            // Generieren von Schulstunden
            var lessonStarts = new TimeSpan[]
                {
                    new TimeSpan(8,0,0),
                    new TimeSpan(8,50,0),
                    new TimeSpan(9,55,0),
                    new TimeSpan(10,45,0),
                    new TimeSpan(11,45,0),
                    new TimeSpan(12,35,0),
                    new TimeSpan(13,25,0),
                    new TimeSpan(14,25,0),
                    new TimeSpan(15,15,0),
                    new TimeSpan(16,15,0),
                    new TimeSpan(17,10,0),
                    new TimeSpan(17,55,0),
                    new TimeSpan(18,50,0),
                    new TimeSpan(19,35,0),
                    new TimeSpan(20,30,0),
                    new TimeSpan(21,15,0),
                };

            var lessonEnds = new TimeSpan[]
                {
                    new TimeSpan(8,50,0),
                    new TimeSpan(9,40,0),
                    new TimeSpan(10,45,0),
                    new TimeSpan(11,35,0),
                    new TimeSpan(12,35,0),
                    new TimeSpan(13,25,0),
                    new TimeSpan(14,15,0),
                    new TimeSpan(15,15,0),
                    new TimeSpan(16,05,0),
                    new TimeSpan(17,05,0),
                    new TimeSpan(17,55,0),
                    new TimeSpan(18,40,0),
                    new TimeSpan(19,35,0),
                    new TimeSpan(20,20,0),
                    new TimeSpan(21,15,0),
                    new TimeSpan(22,00,0),
                };

            var periods = new List<Period>();
            for (int i = 0; i <= lessonStarts.Length -1; i++)
            {
                periods.Add(new Period()
                {
                    From = new DateTime(2020, 01, 01).Add(lessonStarts[i]),
                    To = new DateTime(2020, 01, 01).Add(lessonEnds[i]),
                });
            }
            Periods.AddRange(periods);
            SaveChanges();

            //
            // Generieren von Schulklassen
            var schoolClasses = new Faker<Schoolclass>()
                .Rules((f, s) =>
                {
                    var departements = new string[] { "HIF", "NBGM", "HWIT", "FIT" };

                    s.Id = f.Random.Int(1, 5) + f.Random.String2(1, "ABCD") + f.Random.ListItem(departements);
                    s.Department = f.Random.ListItem(departements);
                    s.TeacherNavigation = f.Random.ListItem(teachers);

                })
                .Generate(80)
                .ToHashSet()
                .ToList();
            Schoolclasss.AddRange(schoolClasses);
            SaveChanges();

            //
            // Generieren von Schülern
            var pupils = new Faker<Pupil>("de")
                .Rules((f, p) =>
                {
                    p.Account = $"{f.Name.LastName().ToUpper()}{f.Random.Int(10000, 99999)}";
                    p.FirstName = f.Name.FirstName();
                    p.LastName = f.Name.LastName();
                    p.SchoolclassNavigation = f.Random.ListItem(schoolClasses);
                })
                .Generate(1700);
            Pupils.AddRange(pupils);
            SaveChanges();

            //
            // Geerieren von Lesssons
            var lessons = new Faker<Lesson>()
                .Rules((f, l) =>
                {
                    var subjects = new string[] { "POS1", "DBI1", "AM", "D", "E", "BWM", "NVS", "PRE" };
                    var period = f.Random.ListItem(periods);
                    l.Day = f.Random.Enum<Domain.Model.DayOfWeek>();
                    l.Subject = f.Random.ListItem(subjects);
                    l.Room = $"{f.Random.String2(1, "ABCD")}{f.Random.Int(1, 5)}.{f.Random.Int(1, 14).ToString().PadLeft(2, '0')}";

                    l.PeriodNavigation = period;
                    l.SchoolclassNavigation = f.Random.ListItem(schoolClasses);
                    l.TeacherNavigation = f.Random.ListItem(teachers);
                })
                .Generate(2800)
                .GroupBy(l => new { l.SchoolclassNavigation, l.Day, l.PeriodNavigation })
                .Select(g => g.First())
                .ToList();
            Lessons.AddRange(lessons);
            SaveChanges();
        }

        public DbSet<Period> Periods => Set<Period>();

        public DbSet<Lesson> Lessons => Set<Lesson>();

        public DbSet<Test> Tests => Set<Test>();

        public DbSet<Teacher> Teachers => Set<Teacher>();

        public DbSet<Schoolclass> Schoolclasss => Set<Schoolclass>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\Temp\\CodeFirstDemo_5BAIF.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Period>().ToTable("Periods");
            modelBuilder.Entity<Period>().HasKey(c => c.Nr);

            modelBuilder.Entity<Teacher>().Property(t => t.Lastname).HasMaxLength(255).IsRequired();
        }
    }
}
