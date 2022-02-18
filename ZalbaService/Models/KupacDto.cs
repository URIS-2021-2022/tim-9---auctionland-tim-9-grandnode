using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZalbaService.Models
{
    public class KupacDto
    {
        public Guid KupacId { get; set; }
        public string Prioritet { get; set; }
        public string Lice { get; set; }
        public string Povrsina { get; set; }
        public string Uplata { get; set; }
        public string OvlascenoLice { get; set; }
        public bool Zabrana { get; set; }
        public DateTime PocetakZabrane { get; set; }
        public int DuzinaZabrane { get; set; }
        public DateTime PrestanakZabrane { get; set; }
        public string JavnoNadmetanje { get; set; }
    }
}
