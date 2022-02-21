using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.Entities
{
    public class FizickoLice : KupacModel
    {
        //exor od Kupca

        public string JMBG { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public string ToString()
        {
            return "Kupac: { KupacID: " + this.KupacID + ", fiz/pravno " + this.FizPravno + ", " +
                "OstvarenaPovrsina: " + this.OstvarenaPovrsina + ", Zabrana: " + this.Zabrana +
                ", PocetakZabrane: " + this.PocetakZabrane + ", duzina zabrane" + this.DuzinaZabrane
                + ", prestanak zabrane:" + this.PrestanakZabrane + ", ovlasceno lice: " + this.OvlascenoLiceID + ", prioritet: " + this.PrioritetID
                + ", brtel1" + this.BrTel1 + ", ime:" + this.Ime + ", prezime: " + this.Prezime + ", jmbg:" + this.JMBG + ", brtel2" + ", uplata: " + this.UplataID + ", email:" + this.Email + ", id adrese: " + this.AdresaID + ", }";
        }


    }
}
