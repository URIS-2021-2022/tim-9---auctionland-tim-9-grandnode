using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Entities
{
    public class Odvodnjavanje
    {
        [Key]
        public Guid OdvodnjavanjeID { get; set; }
        public string NazivOdvodnjavanja { get; set; }
    }
}
