using Parcela.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class ObradivostRepository : IObradivostRepository
    {
        private readonly ParcelaContext context;
        public ObradivostRepository(ParcelaContext context)
        {
            this.context = context;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public Obradivost GetObradivostById(Guid obradivostId)
        {
            return context.Obradivost.FirstOrDefault(o => o.ObradivostID == obradivostId);
        }

        public List<Obradivost> GetObradivostList()
        {
            return context.Obradivost.ToList();
        }
    }
}
