using AutoMapper;
using LicnostService.Entities;
using LicnostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicnostService.Data
{
    public class LicnostRepository : ILicnostRepository
    {

        private readonly LicnostContext context;
        private readonly IMapper mapper;

        public LicnostRepository(LicnostContext context, IMapper mapper) 
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges() 
        {
            return context.SaveChanges() > 0;
        }

        public Licnost CreateLicnost(Licnost licnost)
        {
            var createdEntity = context.Add(licnost);
            return mapper.Map<Licnost>(createdEntity.Entity);
        }

        public void DeleteLicnost(Guid licnostId)
        {
            Licnost licnost = GetLicnostById(licnostId);
            context.Remove(licnost);
        }

        public Licnost GetLicnostById(Guid licnostId)
        {
            return context.Licnosti.FirstOrDefault(e => e.LicnostId == licnostId);
        }

        public List<Licnost> GetLicnosti(string funkcija = null)
        {
            return context.Licnosti.ToList();
        }

        public void UpdateLicnost(Licnost licnost)
        {
            //Entity framework core prati entitet pa nema potrebe za implementacijom
        }
    }
}
