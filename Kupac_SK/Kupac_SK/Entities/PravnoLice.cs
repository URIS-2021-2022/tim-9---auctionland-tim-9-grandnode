using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.Entities
{
    public class PravnoLice : KupacModel 
    {
        public string Naziv { get; set; }
        public string MatBr { get; set; }
        public string Faks { get; set; }

        [ForeignKey("KontaktOsobaModel")]
        public Guid KontaktOsoba { get; set; }
    }
}
