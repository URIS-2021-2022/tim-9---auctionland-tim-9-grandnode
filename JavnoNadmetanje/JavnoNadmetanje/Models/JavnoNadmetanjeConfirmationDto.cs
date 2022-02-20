using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Models
{
    /// <summary>
    /// DTO za potvrdu javnog nadmetanja
    /// </summary>
    public class JavnoNadmetanjeConfirmationDto
    {
        /// <summary>
        /// ID javnog nadmetanja.
        /// </summary>
        public Guid JavnoNadmetanjeID { get; set; }
    }
}
