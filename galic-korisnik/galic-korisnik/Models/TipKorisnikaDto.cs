using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace galic_korisnik.Models
{
    public class TipKorisnikaDto
    {
        public Guid tipKorisnikaId { get; set; }
        public string uloga { get; set; }
    }
}
