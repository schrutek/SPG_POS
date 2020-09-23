using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.Inheritance.Tests
{
	public class Lehrer : Person
	{
		public string Kuerzel { get; }
		public decimal? Gehalt { get; set; }

		public override bool IstVolljaehrig
		{
			get
			{
				return true;
			}
		}
		/// <summary>
		/// Überschreibt GetAccountname() für Lehrer, sodass diese Implementierung auch
		/// bei einem Typencast auf Person aufgerufen wird.
		/// </summary>
		/// <returns></returns>
		public override string GetAccountname()
		{
			return Zuname.ToLower();
		}
		/// <summary>
		/// Diese Methode ist nur bei Lehrern vorhanden. Um sie aufzurufen, muss das Objekt den
		/// Typ Lehrer haben.
		/// </summary>
		/// <param name="prozent"></param>
		public void ErhoeheGehalt(int prozent)
		{
			Gehalt *= (1 + prozent / 100M);
		}
		/// <summary>
		/// Eigener Konstruktor mit 2 Argumenten, der den Konstruktur von Person aufruft.
		/// </summary>
		/// <param name="nr"></param>
		/// <param name="kuerzel"></param>
		public Lehrer(int nr, string kuerzel) : base(nr)
		{
			Kuerzel = kuerzel;
		}
		/// <summary>
		/// Ruft den eigenen Konstruktor (von Lehrer) auf und übergibt ??? als Kürzel.
		/// </summary>
		/// <param name="nr"></param>
		public Lehrer(int nr) : this(nr, "???")
		{ }
	}
}
