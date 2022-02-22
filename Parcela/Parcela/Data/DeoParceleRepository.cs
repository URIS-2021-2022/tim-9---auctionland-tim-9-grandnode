using AutoMapper;
using Parcela.Entities;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class DeoParceleRepository : IDeoParceleRepository
    {
        
        private readonly ParcelaContext context;
        private readonly IMapper mapper;
        public DeoParceleRepository(ParcelaContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public DeoParcele CreateDeoParcele(DeoParcele deoParcele)
        {
            deoParcele.DeoParceleID = Guid.NewGuid();
            context.DeoParcele.Add(deoParcele);
            context.SaveChanges();

            return mapper.Map<DeoParcele>(deoParcele);
        }

        public void DeleteDeoParcele(Guid deoParceleId)
        {
            context.DeoParcele.Remove(context.DeoParcele.FirstOrDefault(dp => dp.DeoParceleID == deoParceleId));
        }

        public DeoParcele GetDeoParcelaById(Guid deoParceleId)
        {
            return context.DeoParcele.FirstOrDefault(dp => dp.DeoParceleID == deoParceleId);
        }

        public List<DeoParcele> GetDeoParceleList()
        {
            return context.DeoParcele.ToList();
        }


        public void UpdateDeoParcele(DeoParcele deoParcele)
        {   
            //nema potrebe za update

        }
    }
}
