using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicnostService.Models
{
    public class LicnostDto
    {
        /// <summary>
        /// Puno ime ličnosti
        /// </summary>
        public string ImeLicnosti { get; set; }

        /// <summary>
        /// Funkija na kojoj je ličnost
        /// </summary>
        public string Funkcija { get; set; }

    }
}
