using LinqUebung1.App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spg.Ado.Demo.MockModels
{
    public class PupilServiceMockUp
    {
        public void ListPupils()
        {
            PruefDb context = PruefDb.FromMockup();

            var result = context.Pupils.Where(p => p.P_Lastname.StartsWith("C"));

            foreach (Pupil item in result)
            {
                Console.WriteLine($"{item.Id}: {item.P_Lastname}");
            }
        }
    }
}
