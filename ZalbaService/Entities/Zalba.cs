using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZalbaService.Entities
{
    public class Zalba
    {

        [Key]
        public Guid ZalbaId { get; set; } = Guid.NewGuid();

        [ForeignKey("TipZalbe")]
        public Guid TipZalbe { get; set; }

        [Required]
        public DateTime DatumZalbe { get; set; }

        [Required]
        public string PodnosilacZalbe { get; set; }

        [Required]
        public string Razlog { get; set; }

        [Required]
        public string Obrazlozenje { get; set; }

        [Required]
        public DateTime DatumResenja { get; set; }

        [Required]
        public string BrojResenja { get; set; }

        [ForeignKey("StatusZalbe")]
        public Guid StatusZalbe { get; set; }

        [Required]
        public string BrojOdluke { get; set; }

        [ForeignKey("Radnja")]
        public Guid Radnja { get; set; }
    }
}
