using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uplata.Models
{
    /// <summary>
    /// DTO za potvrdu kursne liste
    /// </summary>
    public class KursnaListaConfirmationDto
    {
        /// <summary>
        /// ID kursne liste.
        /// </summary>
        public Guid KursnaListaID { get; set; }
    }
}
