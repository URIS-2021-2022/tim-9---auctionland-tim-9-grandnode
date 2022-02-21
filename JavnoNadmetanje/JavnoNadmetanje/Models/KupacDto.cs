using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Models
{
    public class KupacDto
    {
        public Guid KupacID { get; set; }
        public bool FizPravno { get; set; }
        public string OstvarenaPovrsina { get; set; }
        public bool Zabrana { get; set; }
        public DateTime PocetakZabrane { get; set; }
        public int DuzinaZabrane { get; set; }
        public DateTime PrestanakZabrane { get; set; }
        public Guid? OvlascenoLiceID { get; set; }
        public Guid PrioritetID { get; set; }
        public string Lice { get; set; }

        public string BrTel1 { get; set; }
        public string BrTel2 { get; set; }
        public string AdresaID { get; set; }
        public string UplataID { get; set; }

        public string Email { get; set; }
        public string BrojRacuna { get; set; }
    }
}
