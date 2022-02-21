using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Entities
{
    public class Kultura
    {
        [Key]
        public Guid KulturaID { get; set; }
        public string NazivKulture { get; set; }

        public string OpiaKulture { get; set; }
    }
}
