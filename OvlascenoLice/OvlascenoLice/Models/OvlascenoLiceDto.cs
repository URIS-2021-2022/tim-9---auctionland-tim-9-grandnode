using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvlascenoLice.Models
{
    public class OvlascenoLiceDto
    {
        public Guid OvlascenoLiceID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public string BrojDokumenta { get; set; }
        public string BrojTable { get; set; }
        public Guid AdresaID { get; set; }
    }
}
