using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Models
{
    public class PravnoLiceDto
    {
        public Guid LiceID { get; set; }
        public string Naziv { get; set; }
        public string MaticniBroj { get; set; }
        public AdresaDto Adresa { get; set; }
        public string BrojTelefona1 { get; set; }
        public string BrojTelefona2 { get; set; }
        public string Faks { get; set; }
        public string Email { get; set; }
        public string BrojRacuna { get; set; }
    }
}
