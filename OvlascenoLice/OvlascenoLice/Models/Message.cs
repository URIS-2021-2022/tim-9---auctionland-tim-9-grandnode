using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvlascenoLice.Models
{
    public class Message
    {

        /// <summary>
        /// Naziv servisa
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Metoda
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Detalji
        /// </summary>
        public string Information { get; set; }

        /// <summary>
        /// Greska
        /// </summary>
        public string Error { get; set; }
    }

}
