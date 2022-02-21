using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Uplata.Entities
{
    /// <summary>
    /// Entitet kursna lista
    /// </summary>
    public class KursnaLista
    {
        /// <summary>
        /// ID kursne liste.
        /// </summary>
        [Key]
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
