using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Entities
{
    /// <summary>
    ///Entitet tipa javnog nadmetanja
    /// </summary>
    public class TipJavnogNadmetanja
    {
        /// <summary>
        ///ID tipa javnog nadmetanja.
        /// </summary>
        [Key]
        public Guid TipJavnogNadmetanjaID { get; set; }

        /// <summary>
        ///Naziv tipa javnog nadmetanja.
        /// </summary>
        public string NazivTipaJavnogNadmetanja { get; set; }
    }
}
