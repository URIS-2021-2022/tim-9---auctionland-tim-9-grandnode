using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Models
{
    /// <summary>
    /// DTO za potvrdu licitacije
    /// </summary>
    public class LicitacijaConfirmationDto
    {
        /// <summary>
        ///ID licitacije.
        /// </summary>
        public Guid LicitacijaID { get; set; }
    }
}
