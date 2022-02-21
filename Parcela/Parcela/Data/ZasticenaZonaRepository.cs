using Parcela.Entities;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class ZasticenaZonaRepository : IZasticenaZonaRepository
    {
        private readonly ParcelaContext context;
        private readonly IMapper mapper;
        public ZasticenaZonaRepository(ParcelaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public ZasticenaZona GetZasticenaZonaById(Guid zasticenaZonaId)
        {
            return context.ZasticenaZona.FirstOrDefault(z => z.ZasticenaZonaID==zasticenaZonaId);
        }

        public List<ZasticenaZona> GetZasticenaZonaList()
        {
            return context.ZasticenaZona.ToList();
        }
    }
}
