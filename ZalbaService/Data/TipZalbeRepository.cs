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

        public TipZalbeRepository(ZalbaContext context)
        {
            zalbaContext = context;
        }

        public bool SaveChanges()
        {
            return zalbaContext.SaveChanges() > 0;
        }
        
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
