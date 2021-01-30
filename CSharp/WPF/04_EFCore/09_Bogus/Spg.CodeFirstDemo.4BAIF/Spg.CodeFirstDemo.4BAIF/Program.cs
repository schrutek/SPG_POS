using Spg.CodeFirstDemo._4BAIF.Infrastructure;
using System;

namespace Spg.CodeFirstDemo._4BAIF
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (CodeFirstDemoContext context = new CodeFirstDemoContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Seed();
            }

            Console.WriteLine("All Done!");
        }
    }
}
