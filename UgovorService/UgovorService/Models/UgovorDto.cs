using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorService.Entities;

namespace UgovorService.Models
{
    public class UgovorDto
    {
        /// <summary>
        /// ID ugovora
        /// </summary>

        public Guid UgovorID { get; set; }

        /// <summary>
        /// ID tipa garancije
        /// </summary>

        public Guid TipID { get; set; }


        /// <summary>
        /// ID dokumenta
        /// </summary>
        public Guid DokumentID { get; set; }

        /// <summary>
        /// Zavodni broj ugovora
        /// </summary>

        public string ZavodniBr { get; set; }


        /// <summary>
        /// ID nadmetanja
        /// </summary>
        public Guid JavnoNadmetanjeID { get; set; }

        /// <summary>
        /// Datum zavodjenja dokumenta
        /// </summary>

        public DateTime DatumZavo { get; set; }


        /// <summary>
        /// ID kupca
        /// </summary>
        public Guid KupacID { get; set; }


        /// <summary>
        /// Rok ugovora
        /// </summary>
        public DateTime Rok { get; set; }


        /// <summary>
        /// Mesto potpisivanja ugovora
        /// </summary>
        public string Mesto { get; set; }


        public DokumentDto DokumentDto { get; set; }

        public KupacDto KupacDto { get; set; }

        public JavnoNadmetanjeDto JavnoNadmetanjeDto { get; set; }

        /// <summary>
        /// Datum potpisivanja ugovora
        /// </summary>
        public DateTime DatumPot { get; set; }
    }
}
