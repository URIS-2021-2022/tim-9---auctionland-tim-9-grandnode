using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZalbaService.Models.TipZalbe
{
    public class TipZalbeUpdateDto
    {
        public Guid TipZalbeId { get; set; }

        [Required(ErrorMessage ="Obavezno uneti naziv tipa!")]
        public string NazivTipa { get; set; }
    }
}
