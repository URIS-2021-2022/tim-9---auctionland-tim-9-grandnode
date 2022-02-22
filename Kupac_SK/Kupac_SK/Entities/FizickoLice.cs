using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.Entities
{
    public class FizickoLice : KupacModel
    {
        //exor od Kupca
        public FizickoLice()
        {
          
        }
   
        public FizickoLice(KupacModel kupac)
        {
            KupacID = kupac.KupacID;
            FizPravno = kupac.FizPravno;
            OstvarenaPovrsina = kupac.OstvarenaPovrsina;
            Zabrana = kupac.Zabrana;
            PocetakZabrane = kupac.PocetakZabrane;
            OvlascenoLiceID = kupac.OvlascenoLiceID;
            PrioritetID = kupac.PrioritetID;
            BrTel1 = kupac.BrTel1;
            BrTel2 = kupac.BrTel2;
            AdresaID = kupac.AdresaID;
            UplataID = kupac.UplataID;
            Email = kupac.Email;
            BrojRacuna = kupac.BrojRacuna;

        }
        public string JMBG { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        override
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
