using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Entities
{
    public class Klasa
    {
        [Key]
        public Guid KlasaID { get; set; }
        public string NazivKlase { get; set; }
    }
}
