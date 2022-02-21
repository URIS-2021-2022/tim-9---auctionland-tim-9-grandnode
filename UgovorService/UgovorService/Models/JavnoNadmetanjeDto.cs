using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorService.Models
{
    public class JavnoNadmetanjeDto
    {
        public Guid JavnoNadmetanjeID { get; set; }

        public DateTime Datum { get; set; }

        public int BrojUcesnika { get; set; }
    }
}
