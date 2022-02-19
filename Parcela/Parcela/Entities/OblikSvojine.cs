using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Entities
{
    public class OblikSvojine
    {
        [Key]
        public Guid OblikSvojineID { get; set; }
        public string NazivOblikaSvojine { get; set; }

        public string OpisOblikaSvojine { get; set; }
    }
}
