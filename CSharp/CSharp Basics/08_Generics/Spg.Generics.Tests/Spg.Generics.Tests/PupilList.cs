using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.Generics.Tests
{
    public class PupilList : List<Pupil>
    {
        public int Sum()
        {
            int sum = 0;
            foreach (Pupil item in this)
            {
                sum = sum + item.Hours;
            }
            return sum;
        }
    }
}
