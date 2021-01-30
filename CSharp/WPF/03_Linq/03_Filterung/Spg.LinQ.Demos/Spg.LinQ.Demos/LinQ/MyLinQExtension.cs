using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.LinQ.Demos.LinQ
{
    public static class MyLinQExtension
    {
        public static SchuelerList Filter(this IEnumerable<Schueler> xy, Func<Schueler, bool> predicate)
        {
            SchuelerList resultList = new SchuelerList();
            foreach (Schueler schueler in xy)
            {
                if (predicate(schueler))
                {
                    resultList.Add(schueler);
                }
            }
            return resultList;
        }
    }
}
