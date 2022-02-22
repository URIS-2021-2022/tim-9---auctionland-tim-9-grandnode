using Mesto.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mesto.Data
{
    public class AdresaRepository : IAdresaRepository
    {
        private readonly MestoContext context;
        private readonly IMapper mapper;
       
        public AdresaRepository(MestoContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public Adresa CreateAdresa(Adresa adresa)
        {
            var createdEntity = context.Add(adresa);
            context.SaveChanges();
            return mapper.Map<Adresa>(createdEntity.Entity);
        }

        public void DeleteAdresa(Guid adresaId)
        {
            var adresa = GetAdresaById(adresaId);
            context.Remove(adresa);
            context.SaveChanges();
        }

        public Adresa GetAdresaById(Guid adresaId)
        {
            return context.Adresa.FirstOrDefault(a => a.AdresaId == adresaId);
        }

        public List<Adresa> GetAdresaList()
        {
            return context.Adresa.ToList();
        }

        public void UpdateAdresa(Adresa adresa)
        {
           //nije potrebno implementirati posebno update
        }
    }
}
