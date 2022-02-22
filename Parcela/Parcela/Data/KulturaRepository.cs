using AutoMapper;
using Parcela.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class KulturaRepository : IKulturaRepository
    {
        private readonly ParcelaContext context;
        public KulturaRepository(ParcelaContext context)
        {
            this.context = context;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public Kultura GetKulturaById(Guid kulturaId)
        {
            return context.Kultura.FirstOrDefault(k => k.KulturaID == kulturaId);

        }

        public List<Kultura> GetKulturaList()
        {
            return context.Kultura.ToList();
        }
    }
}
