using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KomisijaService.Models
{
    public class LicnostDto
    {
        public Guid LicnostId { get; set; } 

        public string Ime { get; set; }

        public string Prezime { get; set; }

        public string Funkcija { get; set; }

    }
}
