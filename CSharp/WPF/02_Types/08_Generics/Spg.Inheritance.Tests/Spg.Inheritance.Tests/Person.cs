using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.Inheritance.Tests
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual string GetFullName()
        {
            return $"{Name} Name von Person";
        }
    }
}
