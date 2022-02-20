using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorService.Entities;

namespace UgovorService.Data
{
    public class TipGarancijeRepository : ITipGarancijeRepository
    {
        private readonly UgovorContext context;
        private readonly IMapper mapper;

        public TipGarancijeRepository(UgovorContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public TipGarancijeConfirmation CreateGarancije(TipGarancijeEnt garancija)
        {
            var createdEntity = context.Add(garancija);
            return mapper.Map<TipGarancijeConfirmation>(createdEntity.Entity);
        }

        public void DeleteGarancije(Guid tipID)
        {
            var dokument = GetGarancijeByID(tipID);
            context.Remove(dokument);
        }

        public List<TipGarancijeEnt> GetGarancijes(string Tip= null)
        {
            return context.TipGarancijeEnt.Where(e => (Tip == null)).ToList();
        }

        public TipGarancijeEnt GetGarancijeByID(Guid tipID)
        {
            return context.TipGarancijeEnt.FirstOrDefault(e => e.TipID== tipID);
        }


        public void UpdateGarancije(TipGarancijeEnt garancija)
        {
            //
        }
    }
}
