using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Models
{
    public class ParcelaCreateDto : IValidatableObject
    {
        
        public Guid KorisnikParceleID { get; set; }
        public int Povrsina { get; set; }
        public string BrojParcele { get; set; }
        public Guid KatastarskaOpstinaID { get; set; }
        public string BrojListaNepokretnosti { get; set; }
        public Guid KulturaID { get; set; }
        public Guid KlasaID { get; set; }
        public Guid ObradivostID { get; set; }
        public Guid ZasticenaZonaID { get; set; }
        public Guid OblikSvojineID { get; set; }
        public Guid OdvodnjavanjeID { get; set; }
        public string ObradivostStvarnoStanje { get; set; }
        public string KulturaStvarnoStanje { get; set; }
        public string KlasaStvarnoStanje { get; set; }
        public string ZasticenaZonaStvarnoStanje { get; set; }
        public string OdvodnjavanjeStvarnoStanje { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
