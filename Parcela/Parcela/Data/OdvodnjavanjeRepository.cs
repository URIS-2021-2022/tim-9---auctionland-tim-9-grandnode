using AutoMapper;
using Parcela.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class OdvodnjavanjeRepository : IOdvodnjavanjeRepository
    {
        private readonly ParcelaContext context;
        private readonly IMapper mapper;
        public OdvodnjavanjeRepository(ParcelaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public Odvodnjavanje GetOdvodnjavanjeById(Guid odvodnjavanjeId)
        {
            return context.Odvodnjavanje.FirstOrDefault(o => o.OdvodnjavanjeID == odvodnjavanjeId);
        }

        public List<Odvodnjavanje> GetOdvodnjavanjeList()
        {
            return context.Odvodnjavanje.ToList();
        }
    }
}
