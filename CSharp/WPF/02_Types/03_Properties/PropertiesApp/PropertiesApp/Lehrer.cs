﻿using System;

namespace PropertiesApp
{
    public class Lehrer
    {
        public string Vorname { get; set; } = string.Empty;

        public string Zuname { get; set; }

        public decimal? Bruttogehalt
        {
            get
            {
                return _bruttogehalt;
            }
            set
            {
                if (!_bruttogehalt.HasValue)
                {
                    _bruttogehalt = value;
                }
            }
        }
        private decimal? _bruttogehalt;

        public string Kuerzel
        {
            get
            {
                if (Zuname?.Length <= 3)
                {
                    return Zuname?.ToUpper();
                }
                return Zuname?.Substring(0, 3)?.ToUpper() ?? String.Empty;
            }
        }

        public decimal Nettogehalt
        {
            get
            {
                if (_bruttogehalt.HasValue)
                {
                    return _bruttogehalt.Value * 0.8M;
                }
                return 0;


                //return _bruttogehalt ?? _bruttogehalt * 0.8M;
            }
        }

        // Alternativ
        //public decimal Nettogehalt => (_bruttogehalt ?? 0) * 0.8M;
    }
}