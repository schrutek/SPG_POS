using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.Inheritance.Tests
{
	public class Schueler : Person
	{
		public DateTime GebDat { get; set; }

		/// <summary>
		/// override ist verpflichtend, da es keine Implementierung in Person gibt.
		/// </summary>
		public override bool IstVolljaehrig
		{
			get
			{
				return (DateTime.Now - GebDat).TotalDays / 365.25 >= 18;
			}
		}
		/// <summary>
		/// Blendet HiddenMethod() in person aus. Bei einem Typencast auf Person wird
		/// die implementierung in Person aufgerufen. Das ist selten sinnvoll, daher selten.
		/// Es kommt eigentlich nur vor, wenn Methoden aus bestehenden Frameworkklassen,
		/// die nicht mit virtual gekennzeichnet sind, implementiert werden sollen.
		/// </summary>
		/// <returns></returns>
		public new string HiddenMethod()
		{
			return "HiddenMethod in Schueler";
		}
		/// <summary>
		/// Da Person keinen Standardkonstruktur hat, muss hier mit base der gewünschte
		/// Konstruktor aufgerufen werden.
		/// </summary>
		/// <param name="nr"></param>
		public Schueler(int nr) : base(nr)
		{ }
	}
}
