using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dokument_AK.Models
{
    public class DokumentUpdateDto
    {

        /// <summary>
        /// ID dokumenta
        /// </summary>
        public Guid DokumentID { get; set; }


        /// <summary>
        /// ID statusa dokumenta
        /// </summary>
        public Guid StatusDokID { get; set; }

        /// <summary>
        /// Zavodni broj
        /// </summary>

        [Required(ErrorMessage = "Obavezno je uneti zavodni broj!")]
        public string ZavodniBroj { get; set; }

        /// <summary>
        /// Datum dokumenta
        /// </summary>
        public DateTime Datum { get; set; }

        /// <summary>
        /// Datum donosenja dokumenta
        /// </summary>
        public DateTime DatumDonosenjaDokumenta { get; set; }


        /// <summary>
        /// Validiranje datuma dokumenta
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DatumDonosenjaDokumenta < Datum)
            {
                yield return new ValidationResult("Datum donosenja dokumenta mora biti nakon datuma izdavanja",
                    new[] { "DokumentCreationDto" });
            }
        }
    }
}
