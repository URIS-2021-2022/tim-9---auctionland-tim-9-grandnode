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
        private readonly IMapper mapper;

        public RadnjaRepository(ZalbaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

       /* public Radnja CreateRadnja(Radnja radnja)
        {
            var createdEntity = context.Add(radnja);
            return mapper.Map<Radnja>(createdEntity.Entity);
        }

        public void DeleteRadnja(Guid radnjaId)
        {
            var radnja = GetRadnjaById(radnjaId);
            context.Remove(radnja);
        }*/

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
