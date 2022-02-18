using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZalbaService.Models.TipZalbe
{
    public class TipZalbeCreationDto
    {
        [Required(ErrorMessage ="Obavezno uneti naziv tipa zalbe!")]
        public string NazivTipa { get; set; }
    }
}
