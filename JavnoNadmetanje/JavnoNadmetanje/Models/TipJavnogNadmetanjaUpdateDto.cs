using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Models
{
    /// <summary>
    /// Model za azuriranje tipa javnog nadmetanja
    /// </summary>
    public class TipJavnogNadmetanjaUpdateDto
    {
        /// <summary>
        /// ID tipa javnog nadmetanja.
        /// </summary>
        public Guid TipJavnogNadmetanjaID { get; set; }

        /// <summary>
        ///Naziv tipa javnog nadmetanja.
        /// </summary>
        public string NazivTipaJavnogNadmetanja { get; set; }
    }
}
