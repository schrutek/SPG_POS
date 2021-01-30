using System;

namespace Spg.TypeExercise
{
    public class TypeExercise
    {
        /// <summary>
        /// Geben Sie die Fäche des Rechtecks (Länge x Breite) zurück.
        /// Wenn ein Wert (Länge oder Breite) null ist, soll das Ergebnis
        /// auch null sein. Ersetzen Sie ? durch den korrekten Datentyp.
        /// </summary>
        public double? BerechneFlaeche(double? laenge, double? breite)
        {
            return laenge * breite;
        }

        /// <summary>
        /// Geben Sie die Fäche des Rechtecks (Länge x Breite) zurück.
        /// Wenn ein Wert (Länge oder Breite) null ist, soll das Ergebnis
        /// 0 sein.
        /// </summary>
        public double BerechneFlaeche2(double? laenge, double? breite)
        {
            //return (laenge ?? 0) * (breite ?? 0);
            return (laenge * breite) ?? 0;
        }

        /// <summary>
        /// Berechnen Sie den Preis nach folgender Vorschrift:
        /// In steuerProdukt und steuerKategorie sind Steuersätze als Faktoren
        /// gespeichert, also 1.2 für 20%. Sie müssen daher bei der Berechnung
        /// nur den Preis mit diesem Wert multiplizieren.
        /// 
        /// Ist ein Wert für steuerProdukt gesetzt (nicht null), so ist nur dieser
        /// Wert für die Berechnung heranzuziehen (also nettopreis x steuerProdukt).
        /// 
        /// Ist kein Wert für steuerProdukt gesetzt, so ist der Wert in steuerKategorie
        /// heranzuziehen (nettopreis x steuerKategorie).
        /// 
        /// Sind beide Werte nicht gesetzt, ist der Nettopreis x 1.2 zurückzugeben.
        /// 
        /// Verwenden Sie den ?? Operator.
        /// </summary>
        public decimal BerechnePreis(decimal nettopreis, decimal? steuerProdukt, decimal? steuerKategorie)
        {
            //if (!steuerProdukt.HasValue && !steuerKategorie.HasValue)
            //{
            //    return nettopreis * 1.2M;
            //}

            //if (steuerProdukt.HasValue)
            //{
            //    return nettopreis * steuerProdukt.Value;
            //}
            //else
            //{
            //    return nettopreis * steuerKategorie.Value;
            //}

            return 
                nettopreis * steuerProdukt ?? 
                nettopreis * steuerKategorie ?? 
                nettopreis * 1.2M;
        }

        /// <summary>
        /// Geben Sie die durchschnittliche Schülerzahl pro Klasse zurück. Sie
        /// berechnet sich aus schuelerGesamt / klassenGesamt.
        /// </summary>
        public double BerechneSchuelerProKlasse(int schuelerGesamt, int klassenGesamt)
        {
            return schuelerGesamt / (double)klassenGesamt;
        }

        /// <summary>
        /// Geben Sie ein Achtel (also wert / 8) des übergebenen Wertes
        /// zurück. Achten Sie auf den Datentyp des Rückgabewertes.
        /// Kann in dieser Funktion eine Exception auftreten?
        /// </summary>
        public int BerechneAchtel(long wert)
        {
            return (int)(wert / 8);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            TypeExercise typeExercise = new TypeExercise();

            Console.WriteLine("BerechneFlaeche(3,4):              " + typeExercise.BerechneFlaeche(3, 4));
            Console.WriteLine("BerechneFlaeche(3,null):           " + typeExercise.BerechneFlaeche(3, null));
            Console.WriteLine("BerechneFlaeche2(3,null):          " + typeExercise.BerechneFlaeche2(3, null));

            Console.WriteLine("BerechnePreis(100,1.2,null):       " + typeExercise.BerechnePreis(100, 1.2M, null));
            Console.WriteLine("BerechnePreis(100,1.2,1.1):        " + typeExercise.BerechnePreis(100, 1.2M, 1.1M));
            Console.WriteLine("BerechnePreis(100,null,1.1):       " + typeExercise.BerechnePreis(100, null, 1.1M));
            Console.WriteLine("BerechnePreis(100,null,null):      " + typeExercise.BerechnePreis(100, null, null));

            Console.WriteLine("BerechneSchuelerProKlasse(100, 6): " + typeExercise.BerechneSchuelerProKlasse(100, 6));
            Console.WriteLine("BerechneAchtel(120):               " + typeExercise.BerechneAchtel(120));


            ReferenceTypes refTypes = new ReferenceTypes();
            //refTypes.DoPersonSomething();


        }
    }
}