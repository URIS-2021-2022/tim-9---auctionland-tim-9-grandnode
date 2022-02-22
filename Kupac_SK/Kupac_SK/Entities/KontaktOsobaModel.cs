using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.Entities
{
    public class KontaktOsobaModel
    {
        [Key]
        public Guid KontaktOsobaID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Funkcija { get; set; }
        public string Telefon { get; set; }

        override
        public string ToString()
        {
            return "Kontakt osoba { KontaktOsobaID: " + this.KontaktOsobaID + ", ime " + this.Ime + ", " +
                "Prezime: " + this.Prezime + ", funkcije: " + this.Funkcija +  ", telefon: "  + this.Telefon + ", }";
        }


    }
}
