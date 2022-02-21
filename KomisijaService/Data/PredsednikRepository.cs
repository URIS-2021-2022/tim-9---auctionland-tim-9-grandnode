using AutoMapper;
using KomisijaService.Data.Interfaces;
using KomisijaService.Entities;
using KomisijaService.Entities.DataContext;
using KomisijaService.Models.Predsednik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KomisijaService.Data
{
    public class PredsednikRepository : IPredsednikRepository
    {
        private readonly KomisijaContext context;
        private readonly IMapper mapper;

        public PredsednikRepository(KomisijaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public PredsednikConfirmationDto CreatePredsednik(Predsednik predsednik)
        {
            var createdEntity = context.Add(predsednik);
            return mapper.Map<PredsednikConfirmationDto>(createdEntity.Entity);
        }

        public void DeletePredsednik(Guid predsednikId)
        {
            var predsednik = GetPredsednikById(predsednikId);
            context.Remove(predsednik);
        }

        public List<Predsednik> GetAllPredsednik()
        {
            return context.Predsednik.ToList();
        }

        public Predsednik GetPredsednikById(Guid predsednikId)
        {
            return context.Predsednik.FirstOrDefault(r => r.PredsednikId == predsednikId);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

    }
}
