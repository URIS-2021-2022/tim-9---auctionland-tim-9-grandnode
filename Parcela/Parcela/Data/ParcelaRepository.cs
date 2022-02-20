using AutoMapper;
using Parcela.Entities;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class ParcelaRepository : IParcelaRepository
    {
        private readonly ParcelaContext context;
        private readonly IMapper mapper;
        public ParcelaRepository(ParcelaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public Parcela.Entities.Parcela CreateParcela(Entities.Parcela parcela)
        {
            parcela.ParcelaID = Guid.NewGuid();

            context.Parcela.Add(parcela);
            context.SaveChanges();

            return mapper.Map<Parcela.Entities.Parcela>(parcela);
        }

        public void DeleteParcela(Guid parcelaId)
        {
            Parcela.Entities.Parcela parcela = GetParcelaById(parcelaId);

            if (parcela == null)
            {
                throw new ArgumentNullException("parcelaId");
            }

            context.Parcela.Remove(parcela);
            context.SaveChanges();

            
        }

        public Entities.Parcela GetParcelaById(Guid parcelaId)
        {
            return context.Parcela.FirstOrDefault(p => p.ParcelaID == parcelaId);
        }

        public List<Entities.Parcela> GetParcelaList()
        {
            return context.Parcela.ToList();
        }
        public Parcela.Entities.Parcela UpdateParcela(Entities.Parcela parcela)
        {
            throw new NotImplementedException();
        }
    }
}
