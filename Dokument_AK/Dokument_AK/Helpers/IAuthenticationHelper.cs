using Dokument_AK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dokument_AK.Helpers
{
     public interface IAuthenticationHelper
     {
        public bool AuthenticatePrincipal(Principal principal);

       public string GenerateJwt(Principal principal);
     }
    
}
