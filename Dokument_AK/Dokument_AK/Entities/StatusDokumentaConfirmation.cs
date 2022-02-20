using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dokument_AK.Entities
{
    public class StatusDokumentaConfirmation
    {
        public Guid StatusDokID { get; set; }
        public bool Usvojen { get; set; }
        public bool Odbijen { get; set; }
        public bool Otvoren { get; set; }
    }
}
