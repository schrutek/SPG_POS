using System;
using System.Collections.Generic;
using System.Text;

namespace LinqUebung1.App.Model
{
    public class PruefDb
    {
        public IList<Pupil> Pupils { get; set; } = new List<Pupil>();
        public IList<Pruefung> Pruefungen { get; set; } = new List<Pruefung>();
        private PruefDb()
        { }
        public static PruefDb FromMockup()
        {
            PruefDb db = new PruefDb();

            db.Pupils.Add(new Pupil() { Id = 1000, P_Lastname = "Elt", Vorname = "Célia", Geschlecht = "w", Klasse = "3AHIF" });
            db.Pupils.Add(new Pupil() { Id = 1001, P_Lastname = "Mattack", Vorname = "Loïca", Geschlecht = "m", Klasse = "3AHIF" });
            db.Pupils.Add(new Pupil() { Id = 1002, P_Lastname = "Nayshe", Vorname = "Eliès", Geschlecht = "m", Klasse = "3BHIF" });
            db.Pupils.Add(new Pupil() { Id = 1003, P_Lastname = "Domanek", Vorname = "Noémie", Geschlecht = "m", Klasse = "3CHIF" });
            db.Pupils.Add(new Pupil() { Id = 1004, P_Lastname = "Avramovitz", Vorname = "Chloé", Geschlecht = "m", Klasse = "3BHIF" });
            db.Pupils.Add(new Pupil() { Id = 1005, P_Lastname = "Curtin", Vorname = "Maëline", Geschlecht = "m", Klasse = "3CHIF" });
            db.Pupils.Add(new Pupil() { Id = 1006, P_Lastname = "Riseborough", Vorname = "Lauréna", Geschlecht = "m", Klasse = "3AHIF" });
            db.Pupils.Add(new Pupil() { Id = 1007, P_Lastname = "Kynge", Vorname = "Valérie", Geschlecht = "m", Klasse = "3CHIF" });
            db.Pupils.Add(new Pupil() { Id = 1008, P_Lastname = "Dibden", Vorname = "Maéna", Geschlecht = "m", Klasse = "3CHIF" });
            db.Pupils.Add(new Pupil() { Id = 1009, P_Lastname = "Pinder", Vorname = "Jú", Geschlecht = "m", Klasse = "3BHIF" });
            db.Pupils.Add(new Pupil() { Id = 1010, P_Lastname = "Cuseick", Vorname = "Cléa", Geschlecht = "w", Klasse = "3CHIF" });
            db.Pupils.Add(new Pupil() { Id = 1011, P_Lastname = "Calladine", Vorname = "Clémence", Geschlecht = "m", Klasse = "3CHIF" });
            db.Pupils.Add(new Pupil() { Id = 1012, P_Lastname = "Stiff", Vorname = "Maéna", Geschlecht = "w", Klasse = "3AHIF" });
            db.Pupils.Add(new Pupil() { Id = 1013, P_Lastname = "Elbourn", Vorname = "Josée", Geschlecht = "m", Klasse = "3AHIF" });
            db.Pupils.Add(new Pupil() { Id = 1014, P_Lastname = "Fosdike", Vorname = "Kallisté", Geschlecht = "w", Klasse = "3AHIF" });
            db.Pupils.Add(new Pupil() { Id = 1015, P_Lastname = "Wilton", Vorname = "Lèi", Geschlecht = "m", Klasse = "3CHIF" });
            db.Pupils.Add(new Pupil() { Id = 1016, P_Lastname = "Billson", Vorname = "Eléa", Geschlecht = "m", Klasse = "3BHIF" });
            db.Pupils.Add(new Pupil() { Id = 1017, P_Lastname = "Dunstall", Vorname = "Lyséa", Geschlecht = "m", Klasse = "3AHIF" });
            db.Pupils.Add(new Pupil() { Id = 1018, P_Lastname = "Santori", Vorname = "Céline", Geschlecht = "m", Klasse = "3CHIF" });
            db.Pupils.Add(new Pupil() { Id = 1019, P_Lastname = "Sharpe", Vorname = "Béatrice", Geschlecht = "m", Klasse = "3AHIF" });
            db.Pupils.Add(new Pupil() { Id = 1020, P_Lastname = "Minerdo", Vorname = "Laurélie", Geschlecht = "w", Klasse = "3CHIF" });
            db.Pupils.Add(new Pupil() { Id = 1021, P_Lastname = "Gianulli", Vorname = "Léonie", Geschlecht = "m", Klasse = "3BHIF" });
            db.Pupils.Add(new Pupil() { Id = 1022, P_Lastname = "Works", Vorname = "Styrbjörn", Geschlecht = "m", Klasse = "3CHIF" });
            db.Pupils.Add(new Pupil() { Id = 1023, P_Lastname = "Dixon", Vorname = "Personnalisée", Geschlecht = "m", Klasse = "3BHIF" });
            db.Pupils.Add(new Pupil() { Id = 1024, P_Lastname = "Browne", Vorname = "Esbjörn", Geschlecht = "m", Klasse = "3AHIF" });
            db.Pupils.Add(new Pupil() { Id = 1025, P_Lastname = "Clearley", Vorname = "Åsa", Geschlecht = "m", Klasse = "3CHIF" });
            db.Pupils.Add(new Pupil() { Id = 1026, P_Lastname = "Jeandin", Vorname = "Maïté", Geschlecht = "m", Klasse = "3BHIF" });
            db.Pupils.Add(new Pupil() { Id = 1027, P_Lastname = "McComiskey", Vorname = "Léa", Geschlecht = "w", Klasse = "3CHIF" });
            db.Pupils.Add(new Pupil() { Id = 1028, P_Lastname = "Castellan", Vorname = "Léa", Geschlecht = "m", Klasse = "3AHIF" });
            db.Pupils.Add(new Pupil() { Id = 1029, P_Lastname = "Spurnier", Vorname = "Stéphanie", Geschlecht = "w", Klasse = "3CHIF" });

            db.Pruefungen.Add(db.Pupils[0].AddPruefung(new Pruefung() { Fach = "D", Pruefer = "KY", Note = 4, Datum = new DateTime(2018, 5, 12) }));
            db.Pruefungen.Add(db.Pupils[1].AddPruefung(new Pruefung() { Fach = "AM", Pruefer = "KY", Note = 2, Datum = new DateTime(2018, 3, 10) }));
            db.Pruefungen.Add(db.Pupils[1].AddPruefung(new Pruefung() { Fach = "DBI", Pruefer = "FZ", Note = 1, Datum = new DateTime(2018, 4, 18) }));
            db.Pruefungen.Add(db.Pupils[1].AddPruefung(new Pruefung() { Fach = "DBI", Pruefer = "FZ", Note = 4, Datum = new DateTime(2018, 4, 21) }));
            db.Pruefungen.Add(db.Pupils[1].AddPruefung(new Pruefung() { Fach = "POS", Pruefer = "SZ", Note = 2, Datum = new DateTime(2018, 1, 28) }));
            db.Pruefungen.Add(db.Pupils[1].AddPruefung(new Pruefung() { Fach = "POS", Pruefer = "SZ", Note = 5, Datum = new DateTime(2017, 11, 28) }));
            db.Pruefungen.Add(db.Pupils[10].AddPruefung(new Pruefung() { Fach = "DBI", Pruefer = "FZ", Note = 2, Datum = new DateTime(2018, 2, 2) }));
            db.Pruefungen.Add(db.Pupils[10].AddPruefung(new Pruefung() { Fach = "E", Pruefer = "FAV", Note = 2, Datum = new DateTime(2018, 4, 2) }));
            db.Pruefungen.Add(db.Pupils[10].AddPruefung(new Pruefung() { Fach = "POS", Pruefer = "SZ", Note = 4, Datum = new DateTime(2018, 2, 16) }));
            db.Pruefungen.Add(db.Pupils[11].AddPruefung(new Pruefung() { Fach = "AM", Pruefer = "KY", Note = 5, Datum = new DateTime(2017, 9, 12) }));
            db.Pruefungen.Add(db.Pupils[11].AddPruefung(new Pruefung() { Fach = "D", Pruefer = "KY", Note = 3, Datum = new DateTime(2018, 3, 24) }));
            db.Pruefungen.Add(db.Pupils[11].AddPruefung(new Pruefung() { Fach = "POS", Pruefer = "SZ", Note = 2, Datum = new DateTime(2018, 2, 13) }));
            db.Pruefungen.Add(db.Pupils[12].AddPruefung(new Pruefung() { Fach = "E", Pruefer = "FAV", Note = 4, Datum = new DateTime(2018, 5, 22) }));
            db.Pruefungen.Add(db.Pupils[12].AddPruefung(new Pruefung() { Fach = "POS", Pruefer = "SZ", Note = 5, Datum = new DateTime(2018, 6, 18) }));
            db.Pruefungen.Add(db.Pupils[13].AddPruefung(new Pruefung() { Fach = "AM", Pruefer = "KY", Note = 3, Datum = new DateTime(2018, 2, 4) }));
            db.Pruefungen.Add(db.Pupils[13].AddPruefung(new Pruefung() { Fach = "D", Pruefer = "KY", Note = 1, Datum = new DateTime(2017, 9, 20) }));
            db.Pruefungen.Add(db.Pupils[13].AddPruefung(new Pruefung() { Fach = "E", Pruefer = "FAV", Note = 1, Datum = new DateTime(2017, 11, 26) }));
            db.Pruefungen.Add(db.Pupils[14].AddPruefung(new Pruefung() { Fach = "D", Pruefer = "KY", Note = 4, Datum = new DateTime(2018, 6, 22) }));
            db.Pruefungen.Add(db.Pupils[15].AddPruefung(new Pruefung() { Fach = "POS", Pruefer = "SZ", Note = 2, Datum = new DateTime(2017, 12, 9) }));
            db.Pruefungen.Add(db.Pupils[16].AddPruefung(new Pruefung() { Fach = "E", Pruefer = "FAV", Note = 4, Datum = new DateTime(2018, 4, 14) }));
            db.Pruefungen.Add(db.Pupils[17].AddPruefung(new Pruefung() { Fach = "D", Pruefer = "KY", Note = 4, Datum = new DateTime(2017, 9, 3) }));
            db.Pruefungen.Add(db.Pupils[17].AddPruefung(new Pruefung() { Fach = "POS", Pruefer = "SZ", Note = 5, Datum = new DateTime(2017, 10, 24) }));
            db.Pruefungen.Add(db.Pupils[19].AddPruefung(new Pruefung() { Fach = "E", Pruefer = "FAV", Note = 2, Datum = new DateTime(2017, 12, 10) }));
            db.Pruefungen.Add(db.Pupils[19].AddPruefung(new Pruefung() { Fach = "POS", Pruefer = "SZ", Note = 2, Datum = new DateTime(2017, 10, 13) }));
            db.Pruefungen.Add(db.Pupils[2].AddPruefung(new Pruefung() { Fach = "D", Pruefer = "KY", Note = 2, Datum = new DateTime(2017, 12, 8) }));
            db.Pruefungen.Add(db.Pupils[2].AddPruefung(new Pruefung() { Fach = "DBI", Pruefer = "FZ", Note = 5, Datum = new DateTime(2017, 12, 21) }));
            db.Pruefungen.Add(db.Pupils[2].AddPruefung(new Pruefung() { Fach = "E", Pruefer = "FAV", Note = 1, Datum = new DateTime(2018, 3, 31) }));
            db.Pruefungen.Add(db.Pupils[2].AddPruefung(new Pruefung() { Fach = "E", Pruefer = "FAV", Note = 3, Datum = new DateTime(2018, 3, 27) }));
            db.Pruefungen.Add(db.Pupils[2].AddPruefung(new Pruefung() { Fach = "E", Pruefer = "FAV", Note = 4, Datum = new DateTime(2018, 6, 19) }));
            db.Pruefungen.Add(db.Pupils[2].AddPruefung(new Pruefung() { Fach = "POS", Pruefer = "SZ", Note = 2, Datum = new DateTime(2017, 11, 27) }));
            db.Pruefungen.Add(db.Pupils[20].AddPruefung(new Pruefung() { Fach = "D", Pruefer = "KY", Note = 3, Datum = new DateTime(2018, 3, 29) }));
            db.Pruefungen.Add(db.Pupils[20].AddPruefung(new Pruefung() { Fach = "DBI", Pruefer = "FZ", Note = 5, Datum = new DateTime(2018, 5, 3) }));
            db.Pruefungen.Add(db.Pupils[22].AddPruefung(new Pruefung() { Fach = "D", Pruefer = "NAI", Note = 3, Datum = new DateTime(2018, 1, 13) }));
            db.Pruefungen.Add(db.Pupils[22].AddPruefung(new Pruefung() { Fach = "DBI", Pruefer = "FZ", Note = 1, Datum = new DateTime(2018, 5, 22) }));
            db.Pruefungen.Add(db.Pupils[23].AddPruefung(new Pruefung() { Fach = "E", Pruefer = "FAV", Note = 4, Datum = new DateTime(2017, 11, 11) }));
            db.Pruefungen.Add(db.Pupils[23].AddPruefung(new Pruefung() { Fach = "POS", Pruefer = "SZ", Note = 2, Datum = new DateTime(2017, 9, 12) }));
            db.Pruefungen.Add(db.Pupils[23].AddPruefung(new Pruefung() { Fach = "POS", Pruefer = "SZ", Note = 3, Datum = new DateTime(2017, 12, 4) }));
            db.Pruefungen.Add(db.Pupils[24].AddPruefung(new Pruefung() { Fach = "AM", Pruefer = "KY", Note = 1, Datum = new DateTime(2018, 1, 12) }));
            db.Pruefungen.Add(db.Pupils[24].AddPruefung(new Pruefung() { Fach = "AM", Pruefer = "KY", Note = 5, Datum = new DateTime(2018, 6, 9) }));
            db.Pruefungen.Add(db.Pupils[24].AddPruefung(new Pruefung() { Fach = "E", Pruefer = "FAV", Note = 1, Datum = new DateTime(2017, 9, 20) }));
            db.Pruefungen.Add(db.Pupils[24].AddPruefung(new Pruefung() { Fach = "E", Pruefer = "FAV", Note = 3, Datum = new DateTime(2018, 3, 26) }));
            db.Pruefungen.Add(db.Pupils[24].AddPruefung(new Pruefung() { Fach = "POS", Pruefer = "SZ", Note = 2, Datum = new DateTime(2017, 9, 19) }));
            db.Pruefungen.Add(db.Pupils[25].AddPruefung(new Pruefung() { Fach = "AM", Pruefer = "KY", Note = 5, Datum = new DateTime(2017, 10, 29) }));
            db.Pruefungen.Add(db.Pupils[25].AddPruefung(new Pruefung() { Fach = "D", Pruefer = "KY", Note = 1, Datum = new DateTime(2018, 4, 15) }));
            db.Pruefungen.Add(db.Pupils[25].AddPruefung(new Pruefung() { Fach = "D", Pruefer = "KY", Note = 2, Datum = new DateTime(2018, 4, 1) }));
            db.Pruefungen.Add(db.Pupils[25].AddPruefung(new Pruefung() { Fach = "E", Pruefer = "FAV", Note = 2, Datum = new DateTime(2017, 10, 27) }));
            db.Pruefungen.Add(db.Pupils[26].AddPruefung(new Pruefung() { Fach = "AM", Pruefer = "KY", Note = 4, Datum = new DateTime(2018, 6, 2) }));
            db.Pruefungen.Add(db.Pupils[26].AddPruefung(new Pruefung() { Fach = "D", Pruefer = "KY", Note = 2, Datum = new DateTime(2018, 3, 6) }));
            db.Pruefungen.Add(db.Pupils[26].AddPruefung(new Pruefung() { Fach = "DBI", Pruefer = "FZ", Note = 2, Datum = new DateTime(2018, 6, 18) }));
            db.Pruefungen.Add(db.Pupils[26].AddPruefung(new Pruefung() { Fach = "POS", Pruefer = "SZ", Note = 1, Datum = new DateTime(2018, 3, 23) }));
            db.Pruefungen.Add(db.Pupils[26].AddPruefung(new Pruefung() { Fach = "POS", Pruefer = "SZ", Note = 2, Datum = new DateTime(2018, 2, 18) }));
            db.Pruefungen.Add(db.Pupils[27].AddPruefung(new Pruefung() { Fach = "D", Pruefer = "NAI", Note = 1, Datum = new DateTime(2017, 10, 22) }));
            db.Pruefungen.Add(db.Pupils[27].AddPruefung(new Pruefung() { Fach = "DBI", Pruefer = "FZ", Note = 4, Datum = new DateTime(2017, 10, 29) }));
            db.Pruefungen.Add(db.Pupils[27].AddPruefung(new Pruefung() { Fach = "POS", Pruefer = "SZ", Note = 2, Datum = new DateTime(2018, 1, 14) }));
            db.Pruefungen.Add(db.Pupils[28].AddPruefung(new Pruefung() { Fach = "D", Pruefer = "KY", Note = 4, Datum = new DateTime(2018, 4, 3) }));
            db.Pruefungen.Add(db.Pupils[28].AddPruefung(new Pruefung() { Fach = "E", Pruefer = "FAV", Note = 1, Datum = new DateTime(2018, 6, 8) }));
            db.Pruefungen.Add(db.Pupils[28].AddPruefung(new Pruefung() { Fach = "E", Pruefer = "FAV", Note = 4, Datum = new DateTime(2018, 5, 28) }));
            db.Pruefungen.Add(db.Pupils[28].AddPruefung(new Pruefung() { Fach = "POS", Pruefer = "SZ", Note = 5, Datum = new DateTime(2017, 9, 28) }));
            db.Pruefungen.Add(db.Pupils[29].AddPruefung(new Pruefung() { Fach = "D", Pruefer = "KY", Note = 1, Datum = new DateTime(2017, 12, 30) }));
            db.Pruefungen.Add(db.Pupils[29].AddPruefung(new Pruefung() { Fach = "D", Pruefer = "NAI", Note = 5, Datum = new DateTime(2018, 5, 18) }));
            db.Pruefungen.Add(db.Pupils[3].AddPruefung(new Pruefung() { Fach = "AM", Pruefer = "KY", Note = 1, Datum = new DateTime(2018, 1, 22) }));
            db.Pruefungen.Add(db.Pupils[3].AddPruefung(new Pruefung() { Fach = "AM", Pruefer = "KY", Note = 4, Datum = new DateTime(2018, 2, 11) }));
            db.Pruefungen.Add(db.Pupils[3].AddPruefung(new Pruefung() { Fach = "AM", Pruefer = "KY", Note = 4, Datum = new DateTime(2018, 6, 18) }));
            db.Pruefungen.Add(db.Pupils[3].AddPruefung(new Pruefung() { Fach = "AM", Pruefer = "KY", Note = 5, Datum = new DateTime(2017, 12, 12) }));
            db.Pruefungen.Add(db.Pupils[3].AddPruefung(new Pruefung() { Fach = "AM", Pruefer = "KY", Note = 5, Datum = new DateTime(2018, 4, 6) }));
            db.Pruefungen.Add(db.Pupils[3].AddPruefung(new Pruefung() { Fach = "DBI", Pruefer = "FZ", Note = 1, Datum = new DateTime(2018, 6, 13) }));
            db.Pruefungen.Add(db.Pupils[4].AddPruefung(new Pruefung() { Fach = "AM", Pruefer = "KY", Note = 3, Datum = new DateTime(2018, 4, 5) }));
            db.Pruefungen.Add(db.Pupils[4].AddPruefung(new Pruefung() { Fach = "D", Pruefer = "NAI", Note = 2, Datum = new DateTime(2017, 12, 23) }));
            db.Pruefungen.Add(db.Pupils[4].AddPruefung(new Pruefung() { Fach = "DBI", Pruefer = "FZ", Note = 5, Datum = new DateTime(2017, 12, 11) }));
            db.Pruefungen.Add(db.Pupils[5].AddPruefung(new Pruefung() { Fach = "AM", Pruefer = "KY", Note = 3, Datum = new DateTime(2017, 11, 30) }));
            db.Pruefungen.Add(db.Pupils[5].AddPruefung(new Pruefung() { Fach = "E", Pruefer = "FAV", Note = 2, Datum = new DateTime(2017, 12, 25) }));
            db.Pruefungen.Add(db.Pupils[5].AddPruefung(new Pruefung() { Fach = "E", Pruefer = "FAV", Note = 3, Datum = new DateTime(2018, 6, 6) }));
            db.Pruefungen.Add(db.Pupils[5].AddPruefung(new Pruefung() { Fach = "POS", Pruefer = "SZ", Note = 2, Datum = new DateTime(2017, 10, 4) }));
            db.Pruefungen.Add(db.Pupils[6].AddPruefung(new Pruefung() { Fach = "AM", Pruefer = "KY", Note = 1, Datum = new DateTime(2017, 10, 19) }));
            db.Pruefungen.Add(db.Pupils[6].AddPruefung(new Pruefung() { Fach = "AM", Pruefer = "KY", Note = 1, Datum = new DateTime(2018, 2, 4) }));
            db.Pruefungen.Add(db.Pupils[6].AddPruefung(new Pruefung() { Fach = "D", Pruefer = "KY", Note = 2, Datum = new DateTime(2017, 12, 30) }));
            db.Pruefungen.Add(db.Pupils[7].AddPruefung(new Pruefung() { Fach = "D", Pruefer = "KY", Note = 3, Datum = new DateTime(2017, 12, 27) }));
            db.Pruefungen.Add(db.Pupils[7].AddPruefung(new Pruefung() { Fach = "DBI", Pruefer = "FZ", Note = 4, Datum = new DateTime(2017, 11, 3) }));
            db.Pruefungen.Add(db.Pupils[7].AddPruefung(new Pruefung() { Fach = "E", Pruefer = "FAV", Note = 4, Datum = new DateTime(2018, 5, 15) }));
            db.Pruefungen.Add(db.Pupils[7].AddPruefung(new Pruefung() { Fach = "E", Pruefer = "FAV", Note = 4, Datum = new DateTime(2018, 5, 17) }));
            db.Pruefungen.Add(db.Pupils[8].AddPruefung(new Pruefung() { Fach = "D", Pruefer = "KY", Note = 4, Datum = new DateTime(2017, 12, 5) }));
            db.Pruefungen.Add(db.Pupils[8].AddPruefung(new Pruefung() { Fach = "D", Pruefer = "KY", Note = 4, Datum = new DateTime(2018, 3, 20) }));
            db.Pruefungen.Add(db.Pupils[8].AddPruefung(new Pruefung() { Fach = "DBI", Pruefer = "FZ", Note = 5, Datum = new DateTime(2017, 12, 3) }));
            db.Pruefungen.Add(db.Pupils[8].AddPruefung(new Pruefung() { Fach = "E", Pruefer = "FAV", Note = 1, Datum = new DateTime(2018, 2, 14) }));
            db.Pruefungen.Add(db.Pupils[8].AddPruefung(new Pruefung() { Fach = "E", Pruefer = "FAV", Note = 4, Datum = new DateTime(2018, 4, 2) }));
            db.Pruefungen.Add(db.Pupils[8].AddPruefung(new Pruefung() { Fach = "POS", Pruefer = "SZ", Note = 4, Datum = new DateTime(2018, 4, 16) }));
            db.Pruefungen.Add(db.Pupils[9].AddPruefung(new Pruefung() { Fach = "D", Pruefer = "KY", Note = 2, Datum = new DateTime(2018, 4, 8) }));
            db.Pruefungen.Add(db.Pupils[9].AddPruefung(new Pruefung() { Fach = "D", Pruefer = "KY", Note = 5, Datum = new DateTime(2018, 1, 4) }));

            return db;
        }
    }

}
