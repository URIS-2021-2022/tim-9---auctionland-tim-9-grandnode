using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Models
{
    /// <summary>
    /// DTO za potvrdu statusa javnog nadmetanja
    /// </summary>
    public class StatusNadmetanjaConfirmationDto
    {
        /// <summary>
        /// ID statusa javnog nadmetanja.
        /// </summary>
        public Guid StatusNadmetanjaID { get; set; }
    }
}
