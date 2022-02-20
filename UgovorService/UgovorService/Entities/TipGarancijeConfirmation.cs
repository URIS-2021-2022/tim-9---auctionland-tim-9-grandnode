using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorService.Entities
{
    public class TipGarancijeConfirmation
    {
        /// <summary>
        /// ID tip garancije
        /// </summary>
        public Guid TipID { get; set; }

        /// <summary>
        /// Naziv tipa garancije
        /// </summary>
        public string Tip { get; set; }
    }
}
