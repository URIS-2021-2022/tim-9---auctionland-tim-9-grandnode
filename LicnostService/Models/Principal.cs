using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicnostService.Models
{
    /// <summary>
    /// Model za autentifikaciju
    /// </summary>
    public class Principal
    {
        /// <summary>
        /// Ime korisnika za autentifikaciju
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Lozinka za autentifikaciju
        /// </summary>
        public string Password { get; set; }
    }
}
