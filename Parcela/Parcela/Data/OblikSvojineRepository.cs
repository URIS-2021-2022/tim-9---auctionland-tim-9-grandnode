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
        private readonly IMapper mapper;
        public OblikSvojineRepository(ParcelaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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
