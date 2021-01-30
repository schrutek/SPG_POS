using Spg.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.Services.Services
{
    public class ShowService
    {
        /// <summary>
        /// Erstellte eine neue Instanz der Klasse <code>ShowService</code>
        /// </summary>
        private static readonly Lazy<ShowService> _instance = new Lazy<ShowService>(() => new ShowService());

        /// <summary>
        /// Der Konstruktor wird private gesetzut. Die Klasse kann nun von 
        /// außen nicht mehr instanziert werden.
        /// </summary>
        private ShowService()
        { }

        /// <summary>
        /// Gib die Instanz der Klasse <code>ShowService</code>zurück.
        /// </summary>
        public static ShowService Instance => _instance.Value;
    }
}
