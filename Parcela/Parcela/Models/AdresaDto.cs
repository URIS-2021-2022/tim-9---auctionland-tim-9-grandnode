using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Models
{
    public class AdresaDto
    {
        public Guid AdresaID { get; set; }
        public string Ulica { get; set; }
        public string Mesto { get; set; }
        public string PostanskiBroj { get; set; }
    }
}
