using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dokument_AK.Models
{
    public class InterniConfirmationDto
    {

        /// <summary>
        /// ID dokumenta
        /// </summary>
        public Guid DokumentID { get; set; }


        /// <summary>
        /// Polje koje oznacava da li je dokument izmenjen
        /// </summary>
        public bool Izmenjen { get; set; }
    }
}
