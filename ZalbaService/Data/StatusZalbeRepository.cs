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

        public StatusZalbeRepository(ZalbaContext context)
        {
            zalbaContext = context;
        }

        public bool SaveChanges()
        {
            return zalbaContext.SaveChanges() > 0;
        }


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
