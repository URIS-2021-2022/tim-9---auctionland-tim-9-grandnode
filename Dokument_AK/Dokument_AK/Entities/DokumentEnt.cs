using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Dokument_AK.Entities
{
    public class DokumentEnt
    {
        [Key]
        public Guid DokumentID { get; set; }

        [Required]
        [ForeignKey("StatusDokumentaEnt")]
        public Guid? StatusDokID { get; set; }
        public StatusDokumentaEnt StatusDokumentaEnt { get; set; }
        public string ZavodniBroj { get; set; }
        public DateTime Datum { get; set; }
        public DateTime DatumDonosenjaDokumenta { get; set; }

        override
       public string ToString()
        {
            return "Dokument: { DokumentID: " + this.DokumentID + ", ID statusa: " + this.StatusDokID + ", Zavodni broj: " + this.ZavodniBroj + ", Datum: " + this.Datum + ", Datum donosenja: " + this.DatumDonosenjaDokumenta + " }";
        }
    }
}
