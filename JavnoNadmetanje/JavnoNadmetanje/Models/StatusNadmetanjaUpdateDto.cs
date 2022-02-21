using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Models
{
    /// <summary>
    /// Model za azuriranje statusa javnog nadmetanja
    /// </summary>
    public class StatusNadmetanjaUpdateDto
    {
        /// <summary>
        /// ID statusa javnog nadmetanja.
        /// </summary>
        public Guid StatusNadmetanjaID { get; set; }

        /// <summary>
        ///Naziv statusa javnog nadmetanja.
        /// </summary>
        public string NazivStatusaNadmetanja { get; set; }
    }
}
