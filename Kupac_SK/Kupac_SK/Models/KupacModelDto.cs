using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.Models
{
    public class KupacModelDto
    {
        public Guid KupacId { get; set; }
        public bool FizPravno { get; set; }
        public string OstvarenaPovrsina { get; set; }
        public bool Zabrana { get; set; }
        public DateTime PocetakZabrane { get; set; }
        public string DuzinaZabrane { get; set; }
        public DateTime PrestanakZabrane { get; set; }
 
        public Guid? OvlascenoLiceID { get; set; }

        [ForeignKey("PrioritetModel")]
        public Guid PrioritetID { get; set; }
        //obelezja zajednicka za Flice i Plice koje prebacujemo ovde 
        public string BrTel1 { get; set; }
        public string BrTel2 { get; set; }
        public string Adresa { get; set; }
        public string Email { get; set; }
        public string BrojRacuna { get; set; }
        public string AdresaID { get; set; } //ss iz VO Adresa info
        public string UplataID { get; set; } //ss iz VO Uplata info

      public OvlascenoLiceDTO OvlascenoLice { get; set; }
        public UplataDTO Uplata { get; set; }

    }
}
