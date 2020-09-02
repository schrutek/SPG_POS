using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VererbungUebung
{
    public class Klasse : List<Schueler>
    {
        private string _id;

        public string Id { get => _id; }

        public Lehrer Kv { get; set; }

        public Klasse(string id)
        {
            _id = id;
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

            if (this.Any(c => c.Nr == newSchueler.Nr))
            {
                throw new InvalidOperationException("Schüler bereits vorhanden!");
            }
            else
            {
                base.Add(newSchueler);
            }
        }

        public override string ToString()
        {
            return $"Klasse: {this.Id} ;  KV: {this.Kv.ToString()} ; Schülerzahl: {this.Count}";
        }
    }
}
