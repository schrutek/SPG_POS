using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.Generics.Tests
{
    public class Teacher : IPerson
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Income { get; set; }

        public int Hours { get; set; }
    }
}
