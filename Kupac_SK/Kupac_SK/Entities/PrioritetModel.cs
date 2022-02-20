using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.Entities
{
    public class PrioritetModel
    {

        [Key]
        public Guid PrioritetID { get; set; }
        public string OpisPrioriteta { get; set; }
    }
}
