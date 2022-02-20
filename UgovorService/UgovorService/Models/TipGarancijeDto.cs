using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorService.Models
{
    public class TipGarancijeDto
    {
        /// <summary>
        /// ID tipa garancije
        /// </summary>
        public Guid TipID { get; set; }

        /// <summary>
        /// Naziv tipa garancije
        /// </summary>

        public string Tip { get; set; }
    }
}
