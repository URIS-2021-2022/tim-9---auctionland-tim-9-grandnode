using AutoMapper;
using KomisijaService.Data.Interfaces;
using KomisijaService.Entities;
using KomisijaService.Entities.DataContext;
using KomisijaService.Models.Clanovi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KomisijaService.Data
{
    public class ClanoviRepository : IClanoviRepository
    {
        private readonly KomisijaContext context;
        private readonly IMapper mapper;

        public ClanoviRepository(KomisijaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public ClanoviConfirmationDto CreateClanovi(Clanovi clanovi)
        {
            var createdEntity = context.Add(clanovi);
            return mapper.Map<ClanoviConfirmationDto>(createdEntity.Entity);
        }

        public void DeleteClanovi(Guid clanoviId)
        {
            var clanovi = GetClanoviById(clanoviId);
            context.Remove(clanovi);
        }

        public List<Clanovi> GetAllClanovi(Guid? komisijaId = null)
        {
            return context.Clanovi
                 .Where(r => (komisijaId == null || r.KomisijaId == komisijaId))
                 .ToList();
        }

        public Clanovi GetClanoviById(Guid clanoviId)
        {
            return context.Clanovi.FirstOrDefault(r => r.ClanoviId == clanoviId);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateClanovi(Clanovi clanovi)
        {
            
        }
    }
}
