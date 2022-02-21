using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OvlascenoLice.Entities
{
    public class OvlascenoLiceModel
    {
        [Key]
        public Guid OvlascenoLiceID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public string BrojDokumenta { get; set; }
        public string BrojTable { get; set; }
        
        /// <summary>
        /// adresa ovlascenog lica koju preuzima iz vo
        /// </summary>
        public Guid AdresaID { get; set; }

        override
       public string ToString()
        {
            return "Ovlasceno lice: { OvlascenoLiceID: " + this.OvlascenoLiceID + ", Ime: " + this.Ime+ ", " +
                "Prezime: " + this.Prezime + ", Broj dokumenta: " + this.BrojDokumenta + 
                ", Broj table: " + this.BrojTable + ", id adrese: " + this.AdresaID + "  }";
        }
    }
}
