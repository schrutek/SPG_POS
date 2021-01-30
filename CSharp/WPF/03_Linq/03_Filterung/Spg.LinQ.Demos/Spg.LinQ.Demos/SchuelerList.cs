using System;
using System.Collections.Generic;

namespace Spg.LinQ.Demos
{
    public class SchuelerList : List<Schueler>
    {
        public SchuelerList Filter(Func<Schueler, bool> predicate)
        {
            SchuelerList resultList = new SchuelerList();
            foreach (Schueler schueler in this)
            {
                if (predicate(schueler))
                {
                    resultList.Add(schueler);
                }
            }
            return resultList;
        }

        public IEnumerable<Schueler> Filter2(Func<Schueler, bool> predicate)
        {
            SchuelerList resultList = new SchuelerList();
            foreach (Schueler schueler in this)
            {
                if (predicate(schueler))
                {
                    yield return schueler;
                }
            }
        }
    }
}