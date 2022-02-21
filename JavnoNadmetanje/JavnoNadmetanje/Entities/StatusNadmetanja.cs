using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Entities
{
    /// <summary>
    /// Entitet statusa javnog nadmetanja
    /// </summary>
    public class StatusNadmetanja
    {
        /// <summary>
        /// ID statusa javnog nadmetanja.
        /// </summary>
        [Key]
        public Guid StatusNadmetanjaID { get; set; }

        /// <summary>
        ///Naziv statusa javnog nadmetanja.
        /// </summary>
        public string NazivStatusaNadmetanja { get; set; }
    }
}
