using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KomisijaService.Models.Komisija
{
    public class KomisijaCreationDto
    {
        
        public string NazivKomisije { get; set; }
        public Guid PredsednikId { get; set; } 
    }
}
