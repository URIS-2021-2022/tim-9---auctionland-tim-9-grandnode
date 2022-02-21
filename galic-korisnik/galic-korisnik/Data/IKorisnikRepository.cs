using galic_korisnik.Entities;
using galic_korisnik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace galic_korisnik.Data
{
    public interface IKorisnikRepository
    {
        List<Korisnik> GetKorisnikList();
        Korisnik GetKorisnikById(Guid korisnikId); //vraca 1 prijavu po id-u
        Korisnik CreateKorisnik(Korisnik korisnik); //kreiranje korisnika
        void UpdateKorisnik(Korisnik korisnik); //update korisnika
        void DeleteKorisnik(Guid korisnikId); //brisanje
        bool SaveChanges();
        public bool UserWithCredentialsExists(string korisnickoIme, string lozinka);

    }
}
