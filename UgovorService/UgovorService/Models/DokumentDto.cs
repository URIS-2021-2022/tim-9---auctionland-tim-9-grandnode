using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorService.Models
{
    public class DokumentDto
    {
        public Guid DokumentID { get; set; }

        public string ZavodniBroj { get; set; }

        public DateTime Datum { get; set; }
    }
}
