using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicnostService.Entities
{
    public class Licnost
    {

        public Guid LicnostId { get; set; } = Guid.NewGuid();
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Funkcija { get; set; }

    }
}
