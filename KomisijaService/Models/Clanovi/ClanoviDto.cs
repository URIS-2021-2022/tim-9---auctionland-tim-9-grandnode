using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KomisijaService.Models.Clanovi
{
    public class ClanoviDto
    {
        public Guid ClanoviId { get; set; }
        public Guid KomisijaId { get; set; }

        public LicnostDto Licnost { get; set; }
    }
}
