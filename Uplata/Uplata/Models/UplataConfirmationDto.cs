using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uplata.Models
{
    /// <summary>
    /// DTO za potvrdu uplate
    /// </summary>
    public class UplataConfirmationDto
    {
        /// <summary>
        /// ID uplate.
        /// </summary>
        public Guid UplataID { get; set; }
    }
}
