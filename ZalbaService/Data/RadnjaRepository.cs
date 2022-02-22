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
    public class RadnjaRepository : IRadnjaRepository
    {
        private readonly ZalbaContext context;
        

        public RadnjaRepository(ZalbaContext context)
        {
            this.context = context;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }


        public List<Radnja> GetAllRadnja(string NazivRadnje = null)
        {
            return context.Radnja
                .Where(r => (NazivRadnje == null || r.NazivRadnje == NazivRadnje))
                .ToList();
        }

        public Radnja GetRadnjaById(Guid radnjaId)
        {
            return context.Radnja.FirstOrDefault(r => r.RadnjaId == radnjaId);
        }


    }
}
