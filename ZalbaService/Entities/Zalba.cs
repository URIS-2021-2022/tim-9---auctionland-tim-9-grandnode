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
        public Guid TipZalbeId { get; set; }
        public TipZalbe TipZalbe { get; set; }

        [Required]
        public DateTime DatumZalbe { get; set; }

        [Required]
        public Guid PodnosilacZalbe { get; set; }

        [Required]
        public string Razlog { get; set; }

        [Required]
        public string Obrazlozenje { get; set; }

        [Required]
        public DateTime DatumResenja { get; set; }

        [Required]
        public string BrojResenja { get; set; }

        [ForeignKey("StatusZalbe")]
        public Guid StatusZalbeId { get; set; }
        public StatusZalbe StatusZalbe { get; set; }

        [Required]
        public string BrojOdluke { get; set; }

        [ForeignKey("Radnja")]
        public Guid RadnjaId { get; set; }
        public Radnja Radnja { get; set; }
    }
}
