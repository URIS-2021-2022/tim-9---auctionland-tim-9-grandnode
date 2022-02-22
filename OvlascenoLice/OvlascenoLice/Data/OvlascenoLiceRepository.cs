using AutoMapper;
using OvlascenoLice.Entities;
using OvlascenoLice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvlascenoLice.Data
{
    public class OvlascenoLiceRepository : IOvlascenoLiceRepository
    {
        public static List<OvlascenoLiceModel> Lica { get; set; } = new List<OvlascenoLiceModel>();

        private readonly OvlascenoLiceContext context;
        private readonly IMapper mapper;


        public OvlascenoLiceRepository(OvlascenoLiceContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
   
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

  
        public OvlascenoLiceModel CreateOvlascenoLice(OvlascenoLiceModel ovlascenoLice)
        {
            var createdEntity = context.Add(ovlascenoLice);
            return mapper.Map<OvlascenoLiceModel>(createdEntity.Entity);
        }

        public void DeleteOvlascenoLice(Guid OLiceID)
        {
            var ovlLice = GetOvlascenoLiceById(OLiceID);
            context.Remove(ovlLice);

        }

        public List<OvlascenoLiceModel> GetOvlascenaLica()
        {
            return context.OvlascenaLica.ToList();
        }

        public OvlascenoLiceModel GetOvlascenoLiceById(Guid OLiceID)
        {
            return context.OvlascenaLica.FirstOrDefault(e => e.OvlascenoLiceID == OLiceID);
        }

        public void UpdateOvlascenoLice(OvlascenoLiceModel ovlascenoLice)
        {
          //
        }
    }
}
