using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.Inheritance.Tests
{
    public class Teacher : Person
    {
        public string Short { get; set; }

        public decimal Income { get; set; }

        public override string GetFullName()
        {
            return base.GetFullName();
            //return $"{Name} Name von Teacher";
        }
    }
}
