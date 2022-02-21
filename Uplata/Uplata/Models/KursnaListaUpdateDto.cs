using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uplata.Models
{
    /// <summary>
    /// Model za azuriranje kursne liste
    /// </summary>
    public class KursnaListaUpdateDto
    {
        /// <summary>
        /// ID kursne liste.
        /// </summary>
        public Guid KursnaListaID { get; set; }

        /// <summary>
        /// Datum.
        /// </summary>
        public DateTime Datum { get; set; }

        /// <summary>
        /// Valuta.
        /// </summary>
        public string Valuta { get; set; }

        /// <summary>
        /// Vrednost.
        /// </summary>
        public float Vrednost { get; set; }
    }
}
