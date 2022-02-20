using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Models
{
    /// <summary>
    /// DTO za potvrdu tipa javnog nadmetanja
    /// </summary>
    public class TipJavnogNadmetanjaConfirmationDto
    {
        /// <summary>
        ///ID tipa javnog nadmetanja.
        /// </summary>
        public Guid TipJavnogNadmetanjaID { get; set; }
    }
}
