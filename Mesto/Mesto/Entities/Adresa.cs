using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mesto.Entities
{
    public class Adresa
    {
        public Guid AdresaId { get; set; }
        public string Ulica { get; set; }
        public string Mesto { get; set; }
        public int PostanskiBroj { get; set; }
        override
        public string ToString()
        {
            return "Adresa: { ID:" + this.AdresaId + " ";
        }
    }
}
