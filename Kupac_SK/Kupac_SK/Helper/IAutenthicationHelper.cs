using Kupac_SK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.Helper
{
   public  interface IAutenthicationHelper
    {
        public interface IAuthenticationHelper
        {
            public bool AuthenticatePrincipal(Principal principal);

            public string GenerateJwt(Principal principal);
        }
    }
}
