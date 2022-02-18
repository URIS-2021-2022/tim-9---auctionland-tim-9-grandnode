using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZalbaService.Entities
{
    public class Radnja
    {

        [Key]
        public Guid RadnjaId { get; set; } = Guid.NewGuid();

        [Required]
        public string NazivRadnje { get; set; }

    }
}
