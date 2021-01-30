using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace LinqUebung1.App.Model
{
    /// <summary>
    /// Speichert die Schülerdaten und eine Liste aller Prüfungen des Schülers.
    /// </summary>
    public class Pupil
    {
        public int Id { get; set; }
        public string P_Lastname { get; set; }
        public string Vorname { get; set; }
        public string Geschlecht { get; set; }
        public string Klasse { get; set; }
        [JsonIgnore]
        public List<Pruefung> Pruefungen { get; set; } = new List<Pruefung>();

        public Pruefung AddPruefung(Pruefung pruefung)
        {
            Pruefungen.Add(pruefung);
            pruefung.Pupil = this;
            return pruefung;
        }
    }
}
