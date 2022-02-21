using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KomisijaService.Entities
{
    public class Clanovi
    {
        [Key]
        public Guid ClanoviId { get; set; } = Guid.NewGuid();
        [ForeignKey("Komisija")]
        public Guid? KomisijaId { get; set; }
        public Komisija Komisija { get; set; }

        override
        public string ToString()
        {
            return "Clanovi: { ClanId: " + this.ClanoviId + ", KomisijaId: " + this.KomisijaId  + " }";
        }
    }
}
