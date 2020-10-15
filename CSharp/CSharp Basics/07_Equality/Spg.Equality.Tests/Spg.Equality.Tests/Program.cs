using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Spg.Equality.Tests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<Engine> engines = new List<Engine>()
            {
                new Engine("VW", 1923, 798),
                new Engine("BMW", 2289, 637),
                new Engine("Chrycler", 5023, 721),
                new Engine("Fiat", 1657, 315),
                new Engine("Peugeot", 2892, 347),
            };

            Console.WriteLine("------ Sort 1 Brand (aufsteigend)  ------");
            engines.Sort();
            foreach (Engine item in engines) Console.WriteLine(item);

            Console.WriteLine("------ Sort 2 Brand (aufsteigend)  ------");
            engines.Sort(new BrandsComparer());
            foreach (Engine item in engines) Console.WriteLine(item);

            Console.WriteLine("------ Sort 3 Brand (aufsteigend)  ------");
            engines.Sort(CompareBrands);
            foreach (Engine item in engines) Console.WriteLine(item);

            Console.WriteLine("------ Sort 4 Brand (aufsteigend)  ------");
            engines.Sort((x, y) =>
            {
                if (x == null)
                {
                    if (y == null)
                    {
                        return 0;
                    }
                    return -1;
                }
                return x.Brand.CompareTo(y.Brand);
            });
            foreach (Engine item in engines) Console.WriteLine(item);
        }

        public static int CompareBrands([AllowNull] Engine x, [AllowNull] Engine y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                return -1;
            }
            return x.Brand.CompareTo(y.Brand);
        }
    }

    public class BrandsComparer : IComparer<Engine>
    {
        public int Compare([AllowNull] Engine x, [AllowNull] Engine y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                return -1;
            }
            return x.Brand.CompareTo(y.Brand);
        }
    }
}
