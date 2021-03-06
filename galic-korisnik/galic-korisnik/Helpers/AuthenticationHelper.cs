using galic_korisnik.Data;
using galic_korisnik.Entities;
using galic_korisnik.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace galic_korisnik.Helpers //: IAuthenticationHelper
{
    public class AuthenticationHelper : IAuthenticationHelper
    {
        private readonly IConfiguration configuration;
        private readonly IKorisnikRepository korisnikRepository;
        private readonly KorisnikContext context;

        public AuthenticationHelper(IConfiguration configuration, IKorisnikRepository korisnikRepository, KorisnikContext context)
        {
            this.configuration = configuration;
            this.korisnikRepository = korisnikRepository;
            this.context = context;
        }

        public bool AuthenticatePrincipal(Principal principal)
        {
            if (korisnikRepository.UserWithCredentialsExists(principal.Username, principal.Password))
            {
                return true;
            }

            return false;
        }

        public string GenerateJwt(Principal principal)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                                             configuration["Jwt:Issuer"],
                                             null,
                                             expires: DateTime.Now.AddMinutes(120),
                                             signingCredentials: credentials);

            Korisnik korisnik = context.Korisnik.FirstOrDefault(e => e.korisnickoIme == principal.Username);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
