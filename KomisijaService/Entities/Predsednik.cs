using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KomisijaService.Entities
{
    public class Predsednik
    {
        [Key]
        public Guid PredsednikId { get; set; } = Guid.NewGuid();

        public override string ToString()
        {
            return "Predsednik: { PredsednikId: " + this.PredsednikId + " }";
        }
    }
}
