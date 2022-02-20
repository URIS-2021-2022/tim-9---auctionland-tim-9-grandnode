using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Models
{
    /// <summary>
    /// Model za kreiranje tipa javnog nadmetanja
    /// </summary>
    public class TipJavnogNadmetanjaCreationDto
    {
        //izbacili smo id jer nam to ne treba prilikom kreiranja jer ce to baza napraviti sama

        /// <summary>
        ///Naziv tipa javnog nadmetanja.
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti naziv tipa javnog nadmetanja.")]
        public string NazivTipaJavnogNadmetanja { get; set; }
    }
}
