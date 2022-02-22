using AutoMapper;
using Kupac_SK.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.Data
{
    public class KontakOsobaRepository : IKontaktOsobaRepository
    {
        private readonly KupacContext context;
        private readonly IMapper mapper;

        public static List<KontaktOsobaModel> KontaktOsobe { get; set; } = new List<KontaktOsobaModel>();
        
        public KontakOsobaRepository(KupacContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
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
           
            var kontakt = context.Add(kontaktOsoba);
            return mapper.Map<KontaktOsobaModel>(kontakt.Entity);
        }
    
        public void DeleteKontaktOsoba(Guid koId)
        {
            var kontakt = GetKontaktOsobaById(koId);
            context.Remove(kontakt);
        }

        public KontaktOsobaModel GetKontaktOsobaById(Guid koId)
        {
            return context.kontaktOsoba.FirstOrDefault(e => e.KontaktOsobaID == koId);
        }

        public List<KontaktOsobaModel> GetKontaktOsobe()
        {
            return context.kontaktOsoba.ToList();
        }

        public void UpdateKontaktOsoba(KontaktOsobaModel kontaktOsoba)
        {
            //
        }
    }
}
