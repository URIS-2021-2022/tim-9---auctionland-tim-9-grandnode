using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.Entities
{
    public class KupacModel
    {   
        [Key]
        public  Guid KupacID { get; set; }
        public bool FizPravno { get; set; } //true - fizicko false - pravno 
        public string OstvarenaPovrsina { get; set; }
        public bool Zabrana { get; set; }
        public DateTime PocetakZabrane { get; set; }
        public string DuzinaZabrane { get; set; }  
        public DateTime PrestanakZabrane { get; set; }
        public Guid? OvlascenoLiceID { get; set; }
        public Guid PrioritetID { get; set; }
        //obelezja zajednicka za Flice i Plice koje prebacujemo ovde 
        public string BrTel1 { get; set; }
        public string BrTel2 { get; set; }
        public string AdresaID { get; set; } //ss iz VO Adresa info
        public string UplataID { get; set; } //ss iz VO Uplata info

        public string Email { get; set; }
        public string BrojRacuna { get; set; }

        override
      public string ToString()
        {
            return "Kupac: { KupacID: " + this.KupacID + ", fiz/pravno " + this.FizPravno + ", " +
                "OstvarenaPovrsina: " + this.OstvarenaPovrsina + ", Zabrana: " + this.Zabrana +
                ", PocetakZabrane: " + this.PocetakZabrane +", duzina zabrane"+  this.DuzinaZabrane
                + ", prestanak zabrane:" + this.PrestanakZabrane+ ", ovlasceno lice: " + this.OvlascenoLiceID + ", prioritet: " + this.PrioritetID
                + ", brtel1" + this.BrTel1 + ", brtel2" + ", uplata: " + this.UplataID + ", email:" + this.Email + ", id adrese: " + this.AdresaID + ", }";
        }

    }
}
