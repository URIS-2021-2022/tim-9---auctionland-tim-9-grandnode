using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Models
{
    public class DeoParceleDto
    {
        public Guid DeoParceleID { get; set; }
        
        public Guid ParcelaID { get; set; }
        public int IdealniDeoParcele { get; set; }
        public int StvarniDeoParcele { get; set; }
    }
}
