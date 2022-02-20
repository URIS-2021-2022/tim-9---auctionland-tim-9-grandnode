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
    public class StatusZalbeRepository : IStatusZalbeRepository
    {

        private readonly ZalbaContext zalbaContext;
        private readonly IMapper mapper;

        public StatusZalbeRepository(ZalbaContext context, IMapper mapper)
        {
            zalbaContext = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return zalbaContext.SaveChanges() > 0;
        }

       /* public StatusZalbe CreateStatusZalbe(StatusZalbe statusZalbe)
        {
            var createdEntity = zalbaContext.Add(statusZalbe);
            return mapper.Map<StatusZalbe>(createdEntity.Entity);
        }

        public void DeleteStatusZalbe(Guid statusZalbeId)
        {
            var statusZalbe = GetStatusZalbeById(statusZalbeId);
            zalbaContext.Remove(statusZalbe);
        }
       */
        public List<StatusZalbe> GetAllStatusZalbe(string NazivStatusa = null)
        {
            return zalbaContext.StatusZalbe
                .Where(sz => (NazivStatusa == null || sz.NazivStatusa == NazivStatusa))
                .ToList();
        }

        public StatusZalbe GetStatusZalbeById(Guid statusZalbeId)
        {
            return zalbaContext.StatusZalbe.FirstOrDefault(sz => sz.StatusZalbeId == statusZalbeId);   
        }


    }
}
