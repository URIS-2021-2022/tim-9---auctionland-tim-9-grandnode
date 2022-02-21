using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorService.Entities;

namespace UgovorService.Data
{
    public class UgovorRepository : IUgovorRepository
    {
        private readonly UgovorContext context;
        private readonly IMapper mapper;

        public UgovorRepository(UgovorContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public UgovorConfirmation CreateUgovor(UgovorEnt ugovor)
        {
            var createdEntity = context.Add(ugovor);
            return mapper.Map<UgovorConfirmation>(createdEntity.Entity);
        }

        public void DeleteUgovor(Guid ugovorID)
        {
            var dokument = GetUgovorByID(ugovorID);
            context.Remove(dokument);
        }

        public List<UgovorEnt> GetUgovors(string ZavodniBr = null)
        {
            return context.UgovorEnt.Where(e => (ZavodniBr == null)).ToList();
        }

        public UgovorEnt GetUgovorByID(Guid ugovorID)
        {
            return context.UgovorEnt.FirstOrDefault(e => e.UgovorID == ugovorID);
        }
    


        public void UpdateUgovor(UgovorEnt ugovor)
        {
            //
        }
    }
}
