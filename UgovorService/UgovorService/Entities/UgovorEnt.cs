using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using UgovorService.Models;

namespace UgovorService.Entities
{
    public class UgovorEnt
    {
        /// <summary>
        /// ID ugovora
        /// </summary>
        [Key]
        public Guid UgovorID { get; set; }

        /// <summary>
        /// ID tipa garancije
        /// </summary>
        [Required]
        [ForeignKey("TipGarancijeEnt")]
        public Guid TipID { get; set; }
        public TipGarancijeEnt TipGarancijeEnt { get; set; }


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


        /// <summary>
        /// Datum potpisivanja ugovora
        /// </summary>
       public DateTime DatumPot { get; set; }

        [NotMapped]
        public DokumentDto DokumentDto { get; set; }

        override
        public string ToString()
        {
            return "Ugovor: { ID: " + this.UgovorID + ", ID garancije: " 
                + this.TipID + ", ID dokumenta: " + this.DokumentID + ", " +
                "Zavodni broj: " + this.ZavodniBr + "," +
                " ID nadmetanja: " + this.JavnoNadmetanjeID + ", " +
                "DAtum zavodjenja: " + this.DatumZavo + "," +
                ", ID kupca: " + this.KupacID + ", " +
                "Rok: " + this.Rok + ", " +
                "Mesto: " + this.Mesto + " }";
        }
    }
}
