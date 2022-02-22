using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZalbaService.Models.Zalba
{
    public class ZalbaUpdateDto
    {
        public Guid ZalbaId { get; set; }

        [Required(ErrorMessage = "Obavezno uneti id tipa zalbe!")]
        public Guid TipZalbeId { get; set; }
        [Required(ErrorMessage = "Obavezno uneti datum zalbe!")]
        public DateTime DatumZalbe { get; set; }
        [Required(ErrorMessage = "Obavezno uneti ime podnosioca zalbe!")]
        public Guid PodnosilacZalbe { get; set; }
        [Required(ErrorMessage = "Obavezno uneti razlog zalbe!")]
        public string Razlog { get; set; }
        [Required(ErrorMessage = "Obavezno uneti obrazlozenje zalbe!")]
        public string Obrazlozenje { get; set; }
        [Required(ErrorMessage = "Obavezno uneti datum resenja!")]
        public DateTime DatumResenja { get; set; }
        [Required(ErrorMessage = "Obavezno uneti naziv broj resenja!")]
        public string BrojResenja { get; set; }
        [Required(ErrorMessage = "Obavezno uneti id statusa zalbe!")]
        public Guid StatusZalbeId { get; set; }
        [Required(ErrorMessage = "Obavezno uneti broj odluke!")]
        public string BrojOdluke { get; set; }
        [Required(ErrorMessage = "Obavezno uneti id radnje!")]
        public Guid RadnjaId { get; set; }
    }
}
