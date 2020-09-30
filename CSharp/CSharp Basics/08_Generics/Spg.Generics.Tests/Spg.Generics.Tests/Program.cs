using System;
using System.Collections.Generic;

namespace Spg.Generics.Tests
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Lösung ohne generische Liste *************************************

            TeacherList techers = new TeacherList()
            {
                new Teacher(){ FirstName="Martin", LastName="Bauer", Income=1230.45M, Hours=12 },
                new Teacher(){ FirstName="Bob", LastName="Meier", Income=789.15M, Hours=26 },
                new Teacher(){ FirstName="Thomas", LastName="Müller", Income=1952.14M, Hours=10 },
                new Teacher(){ FirstName="Michi", LastName="Mayer", Income=93.25M, Hours=6 },
                new Teacher(){ FirstName="Doris", LastName="Hofer", Income=257.45M, Hours=29 },
                new Teacher(){ FirstName="Irene", LastName="Bauer", Income=126.96M, Hours=21 },
                new Teacher(){ FirstName="Sandra", LastName="Wolf", Income=6541.55M, Hours=23 },
                new Teacher(){ FirstName="Sonja", LastName="Simon", Income=364.70M, Hours=22 },
                new Teacher(){ FirstName="Gerhard", LastName="Berger", Income=1347.69M, Hours=14 }
            };

            PupilList pupils = new PupilList()
            {
                new Pupil(){ Name="Name 1", Hours=36 },
                new Pupil(){ Name="Name 2", Hours=34 },
                new Pupil(){ Name="Name 3", Hours=38 },
                new Pupil(){ Name="Name 4", Hours=32 },
            };

            Console.WriteLine($"Summe aller Gehälter: {techers.Sum()}");
            Console.WriteLine($"Summe aller Tickets:  {pupils.Sum()}");


            // Lösung mit generischer Liste *************************************

            ImprovedList<Teacher> techers2 = new ImprovedList<Teacher>()
            {
                new Teacher(){ FirstName="Martin", LastName="Bauer", Income=1230.45M, Hours=12 },
                new Teacher(){ FirstName="Bob", LastName="Meier", Income=789.15M, Hours=26 },
                new Teacher(){ FirstName="Thomas", LastName="Müller", Income=1952.14M, Hours=10 },
                new Teacher(){ FirstName="Michi", LastName="Mayer", Income=93.25M, Hours=6 },
                new Teacher(){ FirstName="Doris", LastName="Hofer", Income=257.45M, Hours=29 },
                new Teacher(){ FirstName="Irene", LastName="Bauer", Income=126.96M, Hours=21 },
                new Teacher(){ FirstName="Sandra", LastName="Wolf", Income=6541.55M, Hours=23 },
                new Teacher(){ FirstName="Sonja", LastName="Simon", Income=364.70M, Hours=22 },
                new Teacher(){ FirstName="Gerhard", LastName="Berger", Income=1347.69M, Hours=14 }
            };

            ImprovedList<Pupil> pupils2 = new ImprovedList<Pupil>()
            {
                new Pupil(){ Name="Name 1", Hours=36 },
                new Pupil(){ Name="Name 2", Hours=34 },
                new Pupil(){ Name="Name 3", Hours=38 },
                new Pupil(){ Name="Name 4", Hours=32 },
            };

            Console.WriteLine($"Summe aller Gehälter: {techers2.Sum()}");
            Console.WriteLine($"Summe aller Tickets:  {pupils2.Sum()}");
        }
    }
}
