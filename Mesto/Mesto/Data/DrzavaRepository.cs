using AutoMapper;
using Mesto.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mesto.Data
{
    public class DrzavaRepository : IDrzavaRepository
    {
        private readonly MestoContext context;
        private readonly IMapper mapper;
        public DrzavaRepository(MestoContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public Drzava CreateDrzava(Drzava drzava)
        {
            var createdEntity = context.Add(drzava);
            return mapper.Map<Drzava>(createdEntity.Entity);
        }

        public void DeleteDrzava(Guid drzavaId)
        {
            var drzava = GetDrzavaById(drzavaId);
            context.Remove(drzava);
        }

        public Drzava GetDrzavaById(Guid drzavaId)
        {
            return context.Drzava.FirstOrDefault(d => d.DrzavaId == drzavaId);
        }

        public List<Drzava> GetDrzavaList()
        {
            return context.Drzava.ToList();
        }

        public void UpdateDrzava(Drzava drzava)
        {
            
        }
    }
}
