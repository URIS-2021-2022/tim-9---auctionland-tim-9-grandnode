using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorService.Entities
{
    public class TipGarancijeEnt
    {
        [Key]
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
