using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Uplata.Models
{
    /// <summary>
    /// Model za kreiranje uplate
    /// </summary>
    public class UplataCreationDto
    {
        //izbacili smo id jer nam to ne treba prilikom kreiranja jer ce to baza napraviti sama

        /// <summary>
        /// Broj racuna.
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti broj racuna.")]
        public string BrojRacuna { get; set; }

        /// <summary>
        /// Poziv na broj.
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti poziv na broj.")]
        public string PozivNaBroj { get; set; }

        /// <summary>
        /// Iznos.
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti iznos.")]
        public float Iznos { get; set; }

        /// <summary>
        /// Svrha uplate.
        /// </summary>
        public string SvrhaUplate { get; set; }

        /// <summary>
        /// Datum.
        /// </summary>
        public DateTime Datum { get; set; }

        /// <summary>
        /// Kursna lista.
        /// </summary>
        public Guid KursnaListaID { get; set; }

        /// <summary>
        /// ID uplatilaca.
        /// </summary>
        public Guid KupacID { get; set; }

        /// <summary>
        /// ID javnog nadmetanja.
        /// </summary>
        public Guid JavnoNadmetanjeID { get; set; }
    }
}
