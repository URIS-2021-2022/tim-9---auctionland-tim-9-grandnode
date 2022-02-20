using Kupac_SK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.Data
{
    public class KontakOsobaRepository : IKontaktOsobaRepository
    {
        public static List<KontaktOsobaModel> KontaktOsobe { get; set; } = new List<KontaktOsobaModel>();
        
        public KontakOsobaRepository()
        {
            FillData();
        }

        private void FillData()
        {
            KontaktOsobe.AddRange(new List<KontaktOsobaModel>
            {
                new KontaktOsobaModel
                {
                      KontaktOsobaID = Guid.Parse("c658a3cf-df57-4818-8a38-00b42bccc8a1"),
                      Ime = "Sara",
                      Prezime ="Kijanovic",
                      Funkcija ="Zastupnik1",
                      Telefon = " 12345687"
                 },
                new KontaktOsobaModel
                {
                       KontaktOsobaID = Guid.Parse("b60955b8-fb83-4947-a72a-ec7050cb3454"),
                      Ime = "Teodora",
                      Prezime ="Kijanovic",
                      Funkcija ="Zastupnik2",
                      Telefon = " 18915517"
                }
            });
        }
        public KontaktOsobaModel CreateKontaktOsoba(KontaktOsobaModel kontaktOsoba)
        {
            kontaktOsoba.KontaktOsobaID = Guid.NewGuid();
            KontaktOsobe.Add(kontaktOsoba);
            KontaktOsobaModel kontakt = GetKontaktOsobaById(kontaktOsoba.KontaktOsobaID);

            return new KontaktOsobaModel
            {
                KontaktOsobaID = kontakt.KontaktOsobaID,
                Ime = kontakt.Ime,
                Prezime = kontakt.Prezime,
                Funkcija = kontakt.Funkcija,
                Telefon = kontakt.Telefon

            };
        }

        public void DeleteKontaktOsoba(Guid koId)
        {
            KontaktOsobe.Remove(KontaktOsobe.FirstOrDefault(e => e.KontaktOsobaID == koId));
        }

        public KontaktOsobaModel GetKontaktOsobaById(Guid koId)
        {
            return KontaktOsobe.FirstOrDefault(e => e.KontaktOsobaID == koId);
        }

        public List<KontaktOsobaModel> GetKontaktOsobe()
        {
            return KontaktOsobe.ToList();
        }

        public void UpdateKontaktOsoba(KontaktOsobaModel kontaktOsoba)
        {
            
        }
    }
}
