using galic_korisnik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace galic_korisnik.Data
{
    public class KorisnikRepository : IKorisnikRepository
    {
        public static List<KorisnikModel> Korisnici { get; set; } = new List<KorisnikModel>();

        public KorisnikRepository()
        {
            FillData();
        }

        private void FillData()
        {
            Korisnici.AddRange(new List<KorisnikModel>
            {
                new KorisnikModel
                {
                    korisnikId = Guid.Parse("f7a20259-5aeb-3135-64ea-32cf7a96b98a"),
                    tipKorisnikaId = Guid.Parse("ce4a6a8a-b25d-d5d0-9364-3dee56521821"),
                    ime = "Petar",
                    prezime = "Petrovic",
                    korisnickoIme = "PPetrovic",
                    lozinka = "123456"
                },
                new KorisnikModel
                {
                    korisnikId = Guid.Parse("e8920f41-e035-da6d-27d1-ee8909f6271d"),
                    tipKorisnikaId = Guid.Parse("22caf793-fbaa-a3f5-8266-7fc3dcc798dc"),
                    ime = "Marko",
                    prezime = "Markovic",
                    korisnickoIme = "MMarkovic",
                    lozinka = "123456"
                }
            });
        }

        public List<KorisnikModel> GetKorisnikList()
        {
            return Korisnici.ToList();
        }

        public KorisnikModel GetKorisnikById(Guid korisnikId)
        {
            return Korisnici.FirstOrDefault(e => e.korisnikId == korisnikId);
        }

        public KorisnikModel CreateKorisnik(KorisnikModel korisnik)
        {
            korisnik.korisnikId = Guid.NewGuid();
            Korisnici.Add(korisnik);
            KorisnikModel temp = GetKorisnikById(korisnik.korisnikId);

            return new KorisnikModel
            {
                korisnikId = temp.korisnikId,
                ime = temp.ime,
                prezime = temp.prezime,
                korisnickoIme = temp.korisnickoIme,
                lozinka = temp.lozinka,
                tipKorisnikaId = temp.tipKorisnikaId
            };
        }

        public KorisnikModel UpdateKorisnik(KorisnikModel korisnik)
        {
            var temp = GetKorisnikById(korisnik.korisnikId);

            temp.korisnikId = korisnik.korisnikId;
            temp.ime = korisnik.ime;
            temp.prezime = korisnik.prezime;
            temp.korisnickoIme = korisnik.korisnickoIme;
            temp.lozinka = korisnik.lozinka;
            temp.tipKorisnikaId = korisnik.tipKorisnikaId;

            return new KorisnikModel
            {
                korisnikId = temp.korisnikId,
                ime = temp.ime,
                prezime = temp.prezime,
                korisnickoIme = temp.korisnickoIme,
                lozinka = temp.lozinka,
                tipKorisnikaId = temp.tipKorisnikaId
            };
        }

        public void DeleteKorisnik(Guid korisnikId)
        {
            Korisnici.Remove(Korisnici.FirstOrDefault(e => e.korisnikId == korisnikId));
        }
    }
}
