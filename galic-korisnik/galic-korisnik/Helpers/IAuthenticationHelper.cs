using galic_korisnik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace galic_korisnik.Helpers
{
    public interface IAuthenticationHelper
    {
        public bool AuthenticatePrincipal(Principal principal);
        public string GenerateJwt(Principal principal);
    }
}
