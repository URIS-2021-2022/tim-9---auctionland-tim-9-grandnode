using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uplata.Models
{
    /// <summary>
    /// DTO za uplatu
    /// </summary>
    public class UplataDto
    {
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
        public Guid KursnaListaID { get; set; }
    }
}
