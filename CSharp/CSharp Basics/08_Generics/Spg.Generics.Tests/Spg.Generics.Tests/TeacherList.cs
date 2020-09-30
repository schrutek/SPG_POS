using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.Generics.Tests
{
    public class TeacherList : List<Teacher>
    {
        public decimal Sum()
        {
            decimal sum = 0;
            foreach (Teacher item in this)
            {
                sum = sum + item.Hours;
            }
            return sum;
        }
    }
}
