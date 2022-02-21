using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Entities
{
    public class Obradivost
    {
        [Key]
        public Guid ObradivostID { get; set; }
        public string NazivObradivosti { get; set; }
    }
}
