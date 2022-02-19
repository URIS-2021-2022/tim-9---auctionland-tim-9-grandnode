using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicnostService.Models
{
    public class LicnostCUDto
    {
        /// <summary>
        /// Identifikator ličnosti
        /// </summary>
        public Guid LicnostId { get; set; }

        /// <summary>
        /// Ime ličnosti
        /// </summary>
        public string Ime { get; set; }

        /// <summary>
        /// Prezime ličnosti
        /// </summary>
        public string Prezime { get; set; }

        /// <summary>
        /// Funkija na kojoj je ličnost
        /// </summary>
        public string Funkcija { get; set; }

    }
}
