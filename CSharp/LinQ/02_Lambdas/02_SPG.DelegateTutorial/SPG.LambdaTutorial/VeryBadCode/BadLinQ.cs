using System;
using System.Collections.Generic;
using System.Text;

namespace SPG.LambdaTutorial.VeryBadCode
{
    public static class BadLinQ
    {
        /// <summary>
        /// My Where Method
        /// </summary>
        /// <param name="data"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static SchuelerList Where(this IEnumerable<Schueler> data, Func<Schueler, bool> predicate)
        {
            SchuelerList resultList = new SchuelerList();
            foreach (Schueler schueler in data)
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
