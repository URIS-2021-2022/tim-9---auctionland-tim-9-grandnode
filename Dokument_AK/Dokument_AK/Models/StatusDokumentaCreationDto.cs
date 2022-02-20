using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dokument_AK.Models
{
    public class StatusDokumentaCreationDto //: IValidatableObject
    {

        /// <summary>
        /// ID statusa dokumenta
        /// </summary>
        public Guid StatusDokID { get; set; }

        /// <summary>
        /// Polje koje oznacava da li je dokument usvojen
        /// </summary>
        public bool Usvojen { get; set; }

        /// <summary>
        /// Polje koje oznacava da li je dokument odbijen
        /// </summary>
        public bool Odbijen { get; set; }

        /// <summary>
        /// Polje koje oznacava da li je dokument otvoren
        /// </summary>
        public bool Otvoren { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Usvojen == true)
        //    {
        //        yield return new ValidationResult("Dokument koji je usvojen ne moze imati druge vrednosti.",
        //            new[] { "StatusDokumentaCreationDto" });
        //    }
        //    if (Odbijen == true)
        //    {
        //        yield return new ValidationResult("Dokument koji je odbijen ne moze imati druge vrednosti.",
        //            new[] { "StatusDokumentaCreationDto" });
        //    }
        //    if (Otvoren == true)
        //    {
        //        yield return new ValidationResult("Dokument koji je otvoren ne moze imati druge vrednosti.",
        //            new[] { "StatusDokumentaCreationDto" });
        //    }
        //}
    }
}
