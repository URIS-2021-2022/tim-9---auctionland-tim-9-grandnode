using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KomisijaService.Entities
{
    public class Komisija
    {
        [Key]
        public Guid KomisijaId { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage ="Obavezno je uneti naziv komisije!")]
        public string NazivKomisije { get; set; }

        [ForeignKey("Predsednik")]
        public Guid? PredsednikId { get; set; } 
        public Predsednik Predsednik { get; set; }

        public string ToString()
        {
            return "Komisija: { KomisijaId: " + this.KomisijaId + ", PredsednikId: " + this.PredsednikId + " }";
        }
    }
}
