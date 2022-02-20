using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dokument_AK.Models
{
    /// <summary>
    /// Predstavlja model dokumenta
    /// </summary>
    public class DokumentDto
    {
        /// <summary>
        /// ID dokumenta
        /// </summary>
        public Guid DokumentID { get; set; }

        public Guid StatusDokID { get; set; }
        /// <summary>
        /// Savodni broj dokumenta
        /// </summary>
        public string ZavodniBroj { get; set; }
        /// <summary>
        /// Datum kreiranja dokumenta
        /// </summary>
        public DateTime Datum { get; set; }

        /// <summary>
        /// Datum donosenja dokumenta
        /// </summary>
        public DateTime DatumDonosenjaDokumenta { get; set; }

    }
}
