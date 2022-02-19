using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Entities
{
    public class DeoParcele
    {
        [Key]
        public Guid DeoParceleID { get; set; }

        [ForeignKey("Parcela")]
        public Guid ParcelaID { get; set; }
        [Required]
        public int IdealniDeoParcele { get; set; }
        public int StvarniDeoParcele { get; set; }
    }
}
