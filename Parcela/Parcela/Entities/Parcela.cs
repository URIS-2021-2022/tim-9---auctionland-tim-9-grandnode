using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Entities
{
    public class Parcela
    {
        [Key]
        public Guid ParcelaID { get; set; }
        public Guid KorisnikParceleID { get; set; }
        public int Povrsina { get; set; }
        public string BrojParcele { get; set; }
        public Guid KatastarskaOpstinaID { get; set; }
        public string BrojListaNepokretnosti { get; set; }
        [ForeignKey("Kultura")]
        public Guid KulturaID { get; set; }
        [ForeignKey("Klasa")]
        public Guid KlasaID { get; set; }
        [ForeignKey("Obradivost")]
        public Guid ObradivostID { get; set; }
        [ForeignKey("ZasticenaZona")]
        public Guid ZasticenaZonaID { get; set; }
        [ForeignKey("OblikSvojine")]
        public Guid OblikSvojineID { get; set; }
        [ForeignKey("Odvodnjavanje")]
        public Guid OdvodnjavanjeID { get; set; }
        public string ObradivostStvarnoStanje { get; set; }
        public string KulturaStvarnoStanje { get; set; }
        public string KlasaStvarnoStanje { get; set; }
        public string ZasticenaZonaStvarnoStanje { get; set; }
        public string OdvodnjavanjeStvarnoStanje { get; set; }
        public List<DeoParcele> ListaDelova { get; set; }
    }
}
