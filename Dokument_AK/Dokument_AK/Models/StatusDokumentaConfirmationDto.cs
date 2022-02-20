using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dokument_AK.Models
{
    public class StatusDokumentaConfirmationDto
    {

        /// <summary>
        /// ID statusa dokumenta
        /// </summary>
        public Guid StatusDokID { get; set; }

        /// <summary>
        /// Polje koje govori da li je dokument usvojen
        /// </summary>
        public bool Usvojen { get; set; }

        /// <summary>
        /// Polje koje oznacava da li je dokumetn odbijen
        /// </summary>
        public bool Odbijen { get; set; }


        /// <summary>
        /// Polje koje oznacava da li je dokument otvoren
        /// </summary>
        public bool Otvoren { get; set; }
    }
}
