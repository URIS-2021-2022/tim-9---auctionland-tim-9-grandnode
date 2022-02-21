using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.Entities
{
    public class PravnoLice : KupacModel 
    {
        public string Naziv { get; set; }
        public string MatBr { get; set; }
        public string Faks { get; set; }
        public Guid KontaktOsoba { get; set; }

        override
        public string ToString()
        {
            return "Kupac: { KupacID: " + this.KupacID + ", fiz/pravno " + this.FizPravno + ", " +
                "OstvarenaPovrsina: " + this.OstvarenaPovrsina + ", Zabrana: " + this.Zabrana +
                ", PocetakZabrane: " + this.PocetakZabrane + ", duzina zabrane" + this.DuzinaZabrane
                + ", prestanak zabrane:" + this.PrestanakZabrane + ", ovlasceno lice: " + this.OvlascenoLiceID + ", prioritet: " + this.PrioritetID
                + ", brtel1" + this.BrTel1 + ", naziv:"+ this.Naziv +",matbr> " + this.MatBr + ", faks:"+ this.Faks + ", brtel2" + ", uplata: " + this.UplataID + ", email:" + this.Email + ", id adrese: " + this.AdresaID + ", }";
        }

    }
}
