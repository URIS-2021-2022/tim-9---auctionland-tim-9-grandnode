using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Entities
{
    public class KatastarskaOpstina
    {
        [Key]
        public Guid KatastarskaOpstinaID { get; set; }
        public string NazivKatastarskeOpstine { get; set; }
    }
}
