using galic_korisnik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace galic_korisnik.Data
{
    public interface IKorisnikRepository
    {
        List<KorisnikModel> GetKorisnikList();

        KorisnikModel GetKorisnikById(Guid korisnikId); //vraca 1 prijavu po id-u

        KorisnikModel CreateKorisnik(KorisnikModel korisnik); //kreiranje korisnika
        KorisnikModel UpdateKorisnik(KorisnikModel korisnik); //update korisnika

        void DeleteKorisnik(Guid korisnikId); //brisanje





        /*List<Korisnik> GetKorisnikList();
        Korisnik GetKorisnikById(Guid korisnikId);
        KorisnikConfirmationDto CreateKorisnik(Korisnik korisnik);
        KorisnikConfirmationDto UpdateKorisnik(Korisnik korisnik);
        KorisnikConfirmationDto DeleteKorisnik(Guid korisnikId);

        bool UserWithCredentialsExists(string korisnickoIme, string lozinka);*/

    }
}
