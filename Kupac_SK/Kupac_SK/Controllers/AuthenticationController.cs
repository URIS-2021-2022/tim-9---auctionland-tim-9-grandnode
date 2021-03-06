using Kupac_SK.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Kupac_SK.Helper.IAutenthicationHelper;

namespace Kupac_SK.Controllers
{/// <summary>
/// kontroler autentifikacije
/// </summary>
    [ApiController]
    [Route("api/kupci")]
    [Produces("application/json", "application/xml")]
    
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationHelper authenticationHelper;
        /// <summary>
        /// 
        ///autentifikacija
        /// </summary>
        /// <param name="authenticationHelper"></param>
        public AuthenticationController(IAuthenticationHelper authenticationHelper)
        {
            this.authenticationHelper = authenticationHelper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        
        public IActionResult Authenticate([FromBody] Principal principal)
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
