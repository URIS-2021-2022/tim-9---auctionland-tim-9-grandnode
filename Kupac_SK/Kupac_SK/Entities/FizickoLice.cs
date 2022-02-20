using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.Entities
{
    public class FizickoLice : KupacModel
    {
        //exor od Kupca

        public string JMBG { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

    }
}
