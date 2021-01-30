using System;
using System.Collections.Generic;
using System.Text;

namespace VererbungUebung
{
    public abstract class Person
    {
        public int Nr { get; }
        public string Zuname { get; set; }
        /// <summary>
        /// Dieses read-only Property ist abstrakt, es muss daher in nachfolgenden Klassen
        /// implementiert werden, wenn sie nicht ebenfalls abstrakt sein sollen.
        /// Es muss jedoch ebenfalls als read-only Property implementiert werden.
        /// </summary>
        public abstract bool IstVolljaehrig { get; }
        /// <summary>
        /// Implementierungen in den abgeleiteten Klassen sollen diese Methode mit override
        /// überschreiben können. Daher müssen wir sie mit virtual kennzeichnen.
        /// </summary>
        /// <returns></returns>
        public virtual string GetAccountname()
        {
            // Substring liefert eine Exception, wenn Zuname kürer als 3 Stellen ist.
            int len = Math.Min(Zuname?.Length ?? 0, 3);
            return $"{Zuname?.Substring(0, len)?.ToLower() ?? ""}{Nr:0000}";
        }
        public Person(int nr)
        {
            Nr = nr;
        }
        public string HiddenMethod()
        {
            return "HiddenMethod in Person";
        }
        /// <summary>
        /// Mit base greifen wir auf die Implementierung von object zu.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string objectToString = base.ToString();
            return $"base.ToString() von Person {Nr} ist {objectToString}";
        }
    }
}
