using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Models
{
    /// <summary>
    /// Model za azuriranje licitacije
    /// </summary>
    public class LicitacijaUpdateDto
    {
        /// <summary>
        ///ID licitacije.
        /// </summary>
        public Guid LicitacijaID { get; set; }

        /// <summary>
        ///Broj.
        /// </summary>
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
