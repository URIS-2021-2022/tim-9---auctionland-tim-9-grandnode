using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dokument_AK.Models
{
    /// <summary>
    /// Predstavlja model internog dokumenta
    /// </summary>
    public class InterniDokumentDto
    {

        /// <summary>
        /// ID dokumenta
        /// </summary>

        public Guid DokumentID { get; set; }

        /// <summary>
        /// Polje koje oznacava da li je dokument promenjen
        /// </summary>
        public bool Izmenjen { get; set; }
        
    }
}
