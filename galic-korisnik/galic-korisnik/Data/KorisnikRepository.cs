using AutoMapper;
using galic_korisnik.Entities;
using galic_korisnik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace galic_korisnik.Data
{
    public class KorisnikRepository : IKorisnikRepository
    {
        private readonly KorisnikContext context;
        private readonly IMapper mapper;

        public List<Korisnik> korisnici { get; set; } = new List<Korisnik>();

        public KorisnikRepository(KorisnikContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;

            korisnici.AddRange(GetKorisnikList());

        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<Korisnik> GetKorisnikList()
        {
            return context.Korisnik.ToList();
        }

        public Korisnik GetKorisnikById(Guid korisnikId)
        {
            return context.Korisnik.FirstOrDefault(e => e.korisnikId == korisnikId);
        }

        public Korisnik CreateKorisnik(Korisnik korisnik)
        {
            korisnik.korisnikId = Guid.NewGuid();

            var lozinka = HashPassword(korisnik.lozinka);

            korisnik.lozinka = lozinka.Item1;
            korisnik.Salt = lozinka.Item2;

            var createdEntity = context.Add(korisnik);
            return mapper.Map<Korisnik>(createdEntity.Entity);
        }

        public void UpdateKorisnik(Korisnik korisnik)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }

        public void DeleteKorisnik(Guid korisnikId)
        {
            var korisnik = GetKorisnikById(korisnikId);
            context.Remove(korisnik);
        }

        public bool Authorize(String token)
        {

            return context.Tokens.FirstOrDefault(e => e.token == token) != null;

        }

        private Tuple<string, string> HashPassword(string lozinka)
        {
            var sBytes = new byte[lozinka.Length];

            new RNGCryptoServiceProvider().GetNonZeroBytes(sBytes);

            var salt = Convert.ToBase64String(sBytes);
            var derivedBytes = new Rfc2898DeriveBytes(lozinka, sBytes, 100);

            return new Tuple<string, string>
            (
                Convert.ToBase64String(derivedBytes.GetBytes(256)),
                salt
            );
        }

        private bool VerifyPassword(string lozinka, string savedLozinka, string savedSalt)
        {
            var saltBytes = Convert.FromBase64String(savedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(lozinka, saltBytes, 100);

            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == savedLozinka;
        }

        public bool UserWithCredentialsExists(string korisnickoIme, string lozinka)
        {
            Korisnik korisnik = korisnici.FirstOrDefault(k => k.korisnickoIme == korisnickoIme);

            if (korisnik == null)
            {
                return false;
            }

            if (VerifyPassword(lozinka, korisnik.lozinka, korisnik.Salt))
            {
                return true;
            }

            return false;
        }
    }
}