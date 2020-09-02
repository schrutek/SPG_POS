using System;
using System.Collections.Generic;
using System.Text;

namespace PropertiesApp
{
    public class Rechteck
    {
        public int Laenge
        {
            get
            {
                return _laenge;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Ungültige Länge!");
                }
                _laenge = value;
            }
        }
        private int _laenge;

        public int Breite
        {
            get
            {
                return _breite;
            }
            set
            {
                _breite = value >= 0 ? value : throw new ArgumentException("Ungültige  Breite!");
            }
        }
        private int _breite;

        public int Flaeche
        {
            get
            {
                return _laenge * _breite;
            }
        }
    }
}
