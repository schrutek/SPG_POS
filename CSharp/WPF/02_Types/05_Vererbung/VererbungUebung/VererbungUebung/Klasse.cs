using System;
using System.Collections.Generic;

namespace VererbungUebung
{
    public class Klasse : List<Schueler>
    {
        public string Id { get; }

        public Lehrer Kv { get; set; }

        public Klasse(string id)
        {
            Id = id;
        }

        public new void Add(Schueler newSchueler)
        {
            foreach (Schueler item in this)
            {
                if (item.Nr == newSchueler.Nr)
                {
                    throw new InvalidOperationException("Schüler bereits vorhanden!");
                }
            }
            base.Add(newSchueler);

            //Alternativ:
            //if (this.Any(c => c.Nr == newSchueler.Nr))
            //{
            //    throw new InvalidOperationException("Schüler bereits vorhanden!");
            //}
            //else
            //{
            //    base.Add(newSchueler);
            //}
        }

        public override string ToString()
        {
            return $"Klasse: {this.Id} ;  KV: {this.Kv.ToString()} ; Schülerzahl: {this.Count}";
        }
    }
}
