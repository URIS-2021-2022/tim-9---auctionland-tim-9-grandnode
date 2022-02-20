using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LicnostService.Entities
{
    /// <summary>
    /// Model ličnosti unutar sistema
    /// </summary>
    public class Licnost
    {
        /// <summary>
        /// Identifikator ličnosti
        /// </summary>
        public Guid LicnostId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Ime ličnosti
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti ime ličnosti")]
        public string Ime { get; set; }

        /// <summary>
        /// Prezime ličnosti
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti prezime ličnosti")]
        public string Prezime { get; set; }

        /// <summary>
        /// Funkija na kojoj je ličnost
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti funkciju ličnosti")]
        public string Funkcija { get; set; }

        override
        public string ToString() 
        {
            return "Licnost: { Ime: " + this.Ime + ", Prezime: " + this.Prezime + ", Funkcija: " + this.Funkcija + " }";
        }

    }
}
