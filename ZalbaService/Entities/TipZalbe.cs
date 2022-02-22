using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZalbaService.Entities
{
    public class TipZalbe
    {
        [Key]
        public Guid TipZalbeId { get; set; } = Guid.NewGuid();

        [Required]
        public string NazivTipa { get; set; }

        public override string ToString()
        {
            return "TipZalbe: { TipZalbeId: " + this.TipZalbeId + ", NazivTipa: " + this.NazivTipa + " }";
        }
    }
}
