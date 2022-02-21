using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Uplata.Models;

namespace Uplata.Entities
{
    /// <summary>
    /// Entitet uplata
    /// </summary>
    public class Uplata
    {
        /// <summary>
        /// ID uplate.
        /// </summary>
        [Key]
        public Guid UplataID { get; set; }

        /// <summary>
        /// Broj racuna.
        /// </summary>
        public string BrojRacuna { get; set; }

        /// <summary>
        /// Poziv na broj.
        /// </summary>
        public string PozivNaBroj { get; set; }

        /// <summary>
        /// Iznos.
        /// </summary>
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
        [ForeignKey("KursnaLista")]
        public Guid KursnaListaID { get; set; }
        public KursnaLista KursnaLista { get; set; }

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
