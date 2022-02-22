using AutoMapper;
using Parcela.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class KlasaRepository : IKlasaRepository
    {
        private readonly ParcelaContext context;
        public KlasaRepository(ParcelaContext context)
        {
            this.context = context;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public Klasa GetKlasaById(Guid klasaId)
        {
            return context.Klasa.FirstOrDefault(k => k.KlasaID == klasaId);
        }

        public List<Klasa> GetKlasaList()
        {
            return context.Klasa.ToList();
        }
    }
}
