using Microsoft.AspNetCore.Mvc;
using Parcela.Helper;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Controllers
{
    [ApiController]
    [Route("api/auth")]
<<<<<<< Updated upstream
=======
    [Produces("application/json")]
>>>>>>> Stashed changes
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationHelper authenticationHelper;

        public AuthenticationController(IAuthenticationHelper authenticationHelper)
        {
            this.authenticationHelper = authenticationHelper;
        }
        [HttpPost("login")]
        public IActionResult Authenticate(Principal principal)
        {
            if (authenticationHelper.AuthenticatePrincipal(principal))
            {
                var tokenString = authenticationHelper.GenerateJwt(principal);
                return Ok(new { token = tokenString });
            }

            return Unauthorized();
        }
    }
}
