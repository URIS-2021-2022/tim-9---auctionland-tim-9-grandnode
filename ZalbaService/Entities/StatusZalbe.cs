using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZalbaService.Entities
{
    public class StatusZalbe
    {

        [Key]
        public Guid StatusZalbeId { get; set; } = Guid.NewGuid();

        [Required]
        public string NazivStatusa { get; set; }

    }
}
