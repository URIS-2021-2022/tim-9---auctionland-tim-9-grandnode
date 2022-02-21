using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uplata.Models
{
    /// <summary>
    /// DTO za kursnu listu
    /// </summary>
    public class KursnaListaDto
    {
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
