using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.Generics.Tests
{
    public class ImprovedList<T> : List<T>
        where T : IPerson, new()
    {
        public decimal Sum()
        {
            decimal sum = 0;
            foreach (T item in this)
            {
                sum = sum + item.Hours;
            }
            return sum;
        }
    }
}
