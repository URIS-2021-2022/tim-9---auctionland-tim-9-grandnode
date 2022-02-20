using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace galic_korisnik.Models
{

    //Vraca podatke korisnika
    public class KorisnikModel
    {
        public Guid korisnikId { get; set; }
        public Guid tipKorisnikaId { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string korisnickoIme { get; set; }
        public string lozinka { get; set; }
    }
}
