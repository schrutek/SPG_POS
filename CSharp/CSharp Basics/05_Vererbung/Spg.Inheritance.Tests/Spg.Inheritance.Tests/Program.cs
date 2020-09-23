using System;

namespace Spg.Inheritance.Tests
{
    public class Program
    {
		// Ausgabe:
		// Ist s1AlsPerson volljährig?          False.
		// Accountname von s1 ist               eif0001.
		// Accountname von l1 ist               fleißig.
		// Accountname von l1AlsPerson ist      fleißig.
		// ToString() von l1 ist                base.ToString() von Person 1 ist VererbungDemo.Lehrer.
		// HiddenMethod von s1AlsPerson ist     HiddenMethod in Person.

		public static void Main(string[] args)
		{
			// Konstruktor und Initializer können gemeinsam verwendet werden.
			Schueler s1 = new Schueler(1) { GebDat = new DateTime(2004, 1, 1), Zuname = "Eiftig" };
			Lehrer l1 = new Lehrer(1) { Gehalt = 3000, Zuname = "Fleißig" };

			// Der Typencast auf Person ist implizit möglich, da eine is-a Beziehung vorliegt.
			Person l1AlsPerson = l1;
			Person s1AlsPerson = s1;

			// Typencast ist verpflichtend, sonst wird ErhoeheGehalt nicht gefunden.
			((Lehrer)l1AlsPerson).ErhoeheGehalt(5000);

			// Ruft die Implementierung in Schueler auf, da ein override existiert.
			Console.WriteLine($"Ist s1AlsPerson volljährig?          {s1AlsPerson.IstVolljaehrig}.");
			// Ruft die Implementierung in Person auf, da kein GetAccountname in Schueler existiert.
			Console.WriteLine($"Accountname von s1 ist               {s1.GetAccountname()}.");
			// Ruft die Implementierung in Lehrer auf, da die Methode dort implementiert wurde.
			Console.WriteLine($"Accountname von l1 ist               {l1.GetAccountname()}.");
			// Ruft die Implementierung in Lehrer auf, da die Methode dort mit override überschrieben wurde.
			Console.WriteLine($"Accountname von l1AlsPerson ist      {l1AlsPerson.GetAccountname()}.");
			// Verwendet ToString in Person, die mit base die ToString Methode von object aufruft.
			Console.WriteLine($"ToString() von l1 ist                {l1.ToString()}.");
			// Ruft HiddenMethod() in Person auf, da sie mit new nur ausgeblendet wurde.
			Console.WriteLine($"HiddenMethod von s1AlsPerson ist     {s1AlsPerson.HiddenMethod()}.");
		}
	}
}
