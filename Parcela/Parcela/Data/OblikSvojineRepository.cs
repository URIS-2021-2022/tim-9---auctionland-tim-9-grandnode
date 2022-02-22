using AutoMapper;
using Parcela.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class OblikSvojineRepository : IOblikSvojineRepository
    {
        private readonly ParcelaContext context;
        public OblikSvojineRepository(ParcelaContext context)
        {
            this.context = context;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public OblikSvojine GetOblikSvojineById(Guid oblikSvojineId)
        {
            return context.OblikSvojine.FirstOrDefault(o => o.OblikSvojineID == oblikSvojineId);
        }

        public List<OblikSvojine> GetOblikSvojineList()
        {
            return context.OblikSvojine.ToList();
        }
    }
}
