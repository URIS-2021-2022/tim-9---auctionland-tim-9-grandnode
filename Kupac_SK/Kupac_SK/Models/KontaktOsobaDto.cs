using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.Models
{
    public class KontaktOsobaDto
    {
        public Guid KontaktOsobaID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Funkcija { get; set; }
        public string Telefon { get; set; }

    }
}
