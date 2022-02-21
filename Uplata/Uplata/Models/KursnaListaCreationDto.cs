using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Uplata.Models
{
    /// <summary>
    /// Model za kreiranje kursne liste
    /// </summary>
    public class KursnaListaCreationDto
    {
        //izbacili smo id jer nam to ne treba prilikom kreiranja jer ce to baza napraviti sama

        /// <summary>
        /// Datum.
        /// </summary>
        public DateTime Datum { get; set; }

        /// <summary>
        /// Valuta.
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti naziv valute.")]
        public string Valuta { get; set; }

        /// <summary>
        /// Vrednost.
        /// </summary>
        public float Vrednost { get; set; }
    }
}
