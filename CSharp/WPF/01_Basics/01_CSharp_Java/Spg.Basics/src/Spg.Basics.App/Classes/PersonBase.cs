using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.Basics.App.Classes
{
    public class PersonBase
    {
        public string FirstName;

        public string LastName;

        public virtual void PrintName()
        {
            Console.Out.WriteLine($"{FirstName} {LastName}");
        }
    }
}
