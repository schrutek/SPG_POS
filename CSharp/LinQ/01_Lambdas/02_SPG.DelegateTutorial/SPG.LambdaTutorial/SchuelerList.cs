using System;
using System.Collections.Generic;
using System.Text;

namespace SPG.LambdaTutorial
{
    public class SchuelerList : List<Schueler>
    {
        public delegate bool FilterHandler(Schueler s);

        public SchuelerList Filter(FilterHandler predicate)
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

        public SchuelerList Where(Func<Schueler, bool> predicate)
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

        public int CalculateSum(Func<int, int, int> predicate)
        {
            return predicate(this[2].Id, this[3].Id);
        }
    }
}
