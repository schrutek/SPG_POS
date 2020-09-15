using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.Basics.App.Classes
{
    public class Person : PersonBase
    {
        public string Vorname;

        public string Nachname;

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

        public void NullableValueTypes()
        {
            int? i = 5;

            i = null;

            if (i.HasValue)
            {
                int j = i.Value;
            }


        }
    }
}
