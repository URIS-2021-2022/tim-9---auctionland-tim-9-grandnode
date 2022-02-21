using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Models
{
    /// <summary>
    /// Model za kreiranje statusa javnog nadmetanja
    /// </summary>
    public class StatusNadmetanjaCreationDto
    {
        //izbacili smo id jer nam to ne treba prilikom kreiranja jer ce to baza napraviti sama

        /// <summary>
        ///Naziv statusa javnog nadmetanja.
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti naziv statusa javnog nadmetanja.")]
        public string NazivStatusaNadmetanja { get; set; }
    }
}
