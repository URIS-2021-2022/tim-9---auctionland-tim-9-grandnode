using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dokument_AK.Entities
{
    public class DokumentConfirmation
    {
        public Guid DokumentID { get; set; }

        public Guid StatusDokID { get; set; }
        public string ZavodniBroj { get; set; }
        public DateTime Datum { get; set; }
        public DateTime DatumDonosenjaDokumenta { get; set; }
    }
}
