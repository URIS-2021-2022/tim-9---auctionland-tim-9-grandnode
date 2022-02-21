using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Entities
{
    public class ZasticenaZona
    {
        [Key]
        public Guid ZasticenaZonaID { get; set; }
        public string NazivZasticeneZone { get; set; }
    }
}
