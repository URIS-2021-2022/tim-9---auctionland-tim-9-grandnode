using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace galic_korisnik.Entities
{
    public class TokenTime
    {
        [Key]
        public Guid korisnikId { get; set; }
        public int tokenId { get; set; }
        public string token { get; set; }
        public DateTime time { get; set; }
    }
}
