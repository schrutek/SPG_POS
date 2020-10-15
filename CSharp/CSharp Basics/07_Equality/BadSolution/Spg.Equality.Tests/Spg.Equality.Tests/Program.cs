using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Spg.Equality.Tests
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Teacher> techers = new List<Teacher>()
            {
                new Teacher(){ FirstName="Martin", LastName="Bauer", Income=1230.45M, Hours=12 },
                new Teacher(){ FirstName="Bob", LastName="Meier", Income=789.15M, Hours=26 },
                new Teacher(){ FirstName="Thomas", LastName="Müller", Income=1952.14M, Hours=10 },
                new Teacher(){ FirstName="Michi", LastName="Mayer", Income=93.25M, Hours=6 },
                new Teacher(){ FirstName="Doris", LastName="Hofer", Income=257.45M, Hours=29 },
                new Teacher(){ FirstName="Irene", LastName="Bauer", Income=126.96M, Hours=21 },
                new Teacher(){ FirstName="Sandra", LastName="Wolf", Income=6541.55M, Hours=23 },
                new Teacher(){ FirstName="Sonja", LastName="Simon", Income=364.70M, Hours=22 },
                new Teacher(){ FirstName="Gerhard", LastName="Berger", Income=1347.69M, Hours=14 },
                new Teacher(){ FirstName="Bob", LastName="Meier", Income=789.15M, Hours=26 },
            };

            List<Pupil> pupils = new List<Pupil>()
            {
                new Pupil(){ Name="Name 1", Hours=36 },
                new Pupil(){ Name="Name 2", Hours=34 },
                new Pupil(){ Name="Name 3", Hours=38 },
                new Pupil(){ Name="Name 4", Hours=32 },
                new Pupil(){ Name="Name 2", Hours=34 },
            };

            Console.WriteLine($"Pupil 1 == Pupil 2:                   {pupils[1] == pupils[4]}");
            Console.WriteLine($"Pupil 1 Equals Pupil 2:               {pupils[1].Equals(pupils[4])}");

            Console.WriteLine($"Teacher 1 == Teacher 2:               {techers[1] == techers[9]}");
            Console.WriteLine($"Teacher 1 Equals Teacher 2:           {techers[1].Equals(techers[9])}");
            Console.WriteLine($"Teacher 1 ReferenceEquals Teacher 2:  {object.ReferenceEquals(techers[1], techers[9])}");

            Console.WriteLine("------ Sort 1  Name gesammt (aufsteigend)  ------");

            techers.Sort();
            foreach (Teacher item in techers) Console.WriteLine(item);

            Console.WriteLine("------ Sort 2  Nachname (aufsteigend)  ------");

            techers.Sort(new Comparer());
            foreach (Teacher item in techers) Console.WriteLine(item);

            Console.WriteLine("------ Sort 3  Vorname (absteigend)  ------");

            //techers.Sort(Compare);

            //techers.Sort(delegate (Teacher x, Teacher y)
            //    {
            //        return (y.FirstName).CompareTo(x?.FirstName);
            //    }
            //);

            techers.Sort((x, y) => (y.FirstName).CompareTo(x?.FirstName));

            foreach (Teacher item in techers) Console.WriteLine(item);
        }

        public static int Compare([AllowNull] Person x, [AllowNull] Person y)
        {
            return (y.FirstName).CompareTo(x?.FirstName);
        }
    }

    public class Comparer : IComparer<Person>
    {
        public int Compare([AllowNull] Person x, [AllowNull] Person y)
        {
            return (x.LastName).CompareTo(y?.LastName);
        }
    }
}
