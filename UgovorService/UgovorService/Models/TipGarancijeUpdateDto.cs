using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorService.Models
{
    public class TipGarancijeUpdateDto
    {
        public Guid TipID { get; set; }

        public string Tip { get; set; }
    }
}
