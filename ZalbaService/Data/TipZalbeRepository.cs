using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZalbaService.Data.Interfaces;
using ZalbaService.Entities;
using ZalbaService.Entities.DataContext;

namespace ZalbaService.Data
{
    public class TipZalbeRepository : ITipZalbeRepository
    {
        private readonly ZalbaContext zalbaContext;
        private readonly IMapper mapper;

        public TipZalbeRepository(ZalbaContext context, IMapper mapper)
        {
            zalbaContext = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return zalbaContext.SaveChanges() > 0;
        }
        /*
        public TipZalbe CreateTipZalbe(TipZalbe tipZalbe)
        {
            var createdEntity = zalbaContext.Add(tipZalbe);
            return mapper.Map<TipZalbe>(createdEntity.Entity);
        }

        public void DeleteTipZalbe(Guid tipZalbeId)
        {
            var tipZalbe = GetTipZalbeById(tipZalbeId);
            zalbaContext.Remove(tipZalbe);
                
        }
        */
        public List<TipZalbe> GetAllTipZalbe(string NazivTipa = null)
        {
            return zalbaContext.TipZalbe
                .Where(tz => (NazivTipa == null || tz.NazivTipa == NazivTipa))
                .ToList();
        }

        public TipZalbe GetTipZalbeById(Guid tipZalbeId)
        {
            return zalbaContext.TipZalbe.FirstOrDefault(tz => tz.TipZalbeId == tipZalbeId);
        }


    }
}
