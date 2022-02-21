using AutoMapper;
using galic_korisnik.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace galic_korisnik.Data
{
    public class TipKorisnikaRepository : ITipKorisnikaRepository
    {

        private readonly KorisnikContext context;
        private readonly IMapper mapper;
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public TipKorisnikaRepository(KorisnikContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public List<TipKorisnika> GetTipKorisnikaList()
        {
            return context.TipKorisnika.ToList();
        }
        public TipKorisnika GetTipKorisnikaById(Guid tipKorisnikaId)
        {
            return context.TipKorisnika.FirstOrDefault(e => e.tipKorisnikaId == tipKorisnikaId);
        }
        public TipKorisnika CreateTipKorisnika(TipKorisnika tipKorisnika)
        {
            var createdEntity = context.Add(tipKorisnika);
            return mapper.Map<TipKorisnika>(createdEntity.Entity);
        }

        public void UpdateTipKorisnika(TipKorisnika tipKorisnika)
        {

        }
        public void DeleteTipKorisnika(Guid tipKorisnikaId)
        {
            var tipKorisnika = GetTipKorisnikaById(tipKorisnikaId);
            context.Remove(tipKorisnika);
        }



    }
}
