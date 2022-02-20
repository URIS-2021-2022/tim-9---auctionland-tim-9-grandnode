using AutoMapper;
using Parcela.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class KatastarskaOpstinaRepository : IKatastarskaOpstinaRepository
    {
        private readonly ParcelaContext context;
        private readonly IMapper mapper;
        public KatastarskaOpstinaRepository(ParcelaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public KatastarskaOpstina GetKatastarskaOpstinaById(Guid KatastarskaOpstinaId)
        {
            return context.KatastarskaOpstina.FirstOrDefault(o => o.KatastarskaOpstinaID == KatastarskaOpstinaId);
        }

        public List<KatastarskaOpstina> GetKatastarskaOpstinas()
        {
            return context.KatastarskaOpstina.ToList();
        }
    }
}
