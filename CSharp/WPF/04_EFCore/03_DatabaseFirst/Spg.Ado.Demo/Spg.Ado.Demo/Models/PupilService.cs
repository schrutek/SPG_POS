using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spg.Ado.Demo.Models
{
    public class PupilService
    {
        public void ListPupils()
        {
            TestsAdministratorContext context = new TestsAdministratorContext();

            IEnumerable<Pupil> result = context.Pupils.Where(p => p.P_Lastname.StartsWith("C"));

            foreach (Pupil item in result)
            {
                Console.WriteLine($"{item.P_ID}: {item.P_Lastname}");
            }
        }
    }
}
