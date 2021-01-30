using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.LinQ.Demos
{
    public static class MyLinQExtension
    {
        public static IEnumerable<Schueler> Filter2(this IEnumerable<Schueler> source, Func<Schueler, bool> predicate)
        {
            SchuelerList resultList = new SchuelerList();
            foreach (Schueler schueler in source)
            {
                if (predicate(schueler))
                {
                    yield return schueler;
                }
            }
        }
    }
}
