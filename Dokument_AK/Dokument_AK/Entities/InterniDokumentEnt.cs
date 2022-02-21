using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Dokument_AK.Entities
{
    public class InterniDokumentEnt
    {
        [Key]
        [ForeignKey("DokumentEnt")]
        public Guid DokumentID { get; set; }
        public bool Izmenjen { get; set; }

    }
}
