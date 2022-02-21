using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Models
{
    /// <summary>
    /// Model za kreiranje licitacije
    /// </summary>
    public class LicitacijaCreationDto
    {
        //izbacili smo id jer nam to ne treba prilikom kreiranja jer ce to baza napraviti sama

        /// <summary>
        ///Broj.
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti broj licitacije.")]
        public int Broj { get; set; }

        /// <summary>
        ///Godina.
        /// </summary>
        public int Godina { get; set; }

        /// <summary>
        ///Datum.
        /// </summary>
        public DateTime Datum { get; set; }

        /// <summary>
        ///Ogranicenja.
        /// </summary>
        public int Ogranicenja { get; set; }

        /// <summary>
        ///Korak cene.
        /// </summary>
        public int KorakCene { get; set; }

        /*/// <summary>
        ///Lista dokumentacije fizicka lica.
        /// </summary>
        public List<string> ListaDokumentacijeFizickaLica { get; set; }

        /// <summary>
        ///Lista dokumentacije pravna lica.
        /// </summary>
        public List<string> ListaDokumentacijePravnaLica { get; set; }*/

        /// <summary>
        ///Javno nadmetanje.
        /// </summary>
        public Guid JavnoNadmetanjeID { get; set; }

        /// <summary>
        ///Rok za dostavljanje prijava.
        /// </summary>
        public DateTime RokPrijava { get; set; }
    }
}
