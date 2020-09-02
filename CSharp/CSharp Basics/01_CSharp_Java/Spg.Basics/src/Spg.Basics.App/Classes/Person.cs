using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.Basics.App.Classes
{
    public class Person : PersonBase
    {
        public override void PrintName()
        {
            Console.Out.WriteLine($"Hello {FirstName} {LastName}!");
        }

        public void PrintName(bool greeting)
        {
            if (greeting)
            {
                Console.Out.WriteLine($"Hello {FirstName} {LastName}! (overridden)");
            }
            else
            {
                base.PrintName();
            }
        }
    }
}
