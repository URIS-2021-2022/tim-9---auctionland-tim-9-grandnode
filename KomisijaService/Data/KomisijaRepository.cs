using AutoMapper;
using KomisijaService.Data.Interfaces;
using KomisijaService.Entities;
using KomisijaService.Entities.DataContext;
using KomisijaService.Models.Komisija;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KomisijaService.Data
{
    public class KomisijaRepository : IKomisijaRepository
    {
        private readonly KomisijaContext context;
        private readonly IMapper mapper;

        public KomisijaRepository(KomisijaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        } 

        public KomisijaConfirmationDto CreateKomisija(Komisija komisija)
        {
            var createdEntity = context.Add(komisija);
            return mapper.Map<KomisijaConfirmationDto>(createdEntity.Entity);
        }

        public void DeleteKomisija(Guid komisijaId)
        {
            var komisija = GetKomisijaById(komisijaId);
            context.Remove(komisija);
        }

        public List<Komisija> GetAllKomisija(Guid? predsednikId = null)
        {
            return context.Komisija
                .Where(r => (predsednikId == null || r.PredsednikId == predsednikId))
                .ToList();
        }

        public Komisija GetKomisijaById(Guid komisijaId)
        {
            return context.Komisija.FirstOrDefault(r => r.KomisijaId == komisijaId);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateKomisija(Komisija komisija)
        {
            
        }
    }
}
