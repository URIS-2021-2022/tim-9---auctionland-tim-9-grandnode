using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Entities
{
    /// <summary>
    ///Entitet licitacije
    /// </summary>
    public class Licitacija
    {
        /// <summary>
        ///ID licitacije.
        /// </summary>
        [Key]
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

        /// <summary>
        ///Lista dokumentacije fizicka lica.
        /// </summary>
        [NotMapped]
        public List<string> ListaDokumentacijeFizickaLica { get; set; }

        /// <summary>
        ///Lista dokumentacije pravna lica.
        /// </summary>
        [NotMapped]
        public List<string> ListaDokumentacijePravnaLica { get; set; }

        /// <summary>
        ///Javno nadmetanje.
        /// </summary>
        [ForeignKey("JavnoNadmetanje")]
        public Guid JavnoNadmetanjeID { get; set; }
        public JavnoNadmetanje JavnoNadmetanje { get; set; }

        /// <summary>
        ///Rok za dostavljanje prijava.
        /// </summary>
        public DateTime RokPrijava { get; set; }
    }
}
