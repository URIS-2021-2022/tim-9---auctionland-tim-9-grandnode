using AutoMapper;
using Kupac_SK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.Data
{
    public class PrioritetRepository : IPrioritetRepository
    {
        private readonly KupacContext context;
        private readonly IMapper mapper;

      //  public static List<PrioritetModel> Prioriteti { get; set; } = new List<PrioritetModel>();

        public PrioritetRepository(KupacContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        /*
        private void FillData()
        {
            Prioriteti.AddRange(new List<PrioritetModel>

            {
                new PrioritetModel
                {
                    PrioritetID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    OpisPrioriteta = "prvi testni prioritet"
                },
                new PrioritetModel
                {
                    PrioritetID = Guid.Parse("32cd906d-8bab-457c-ade2-fbc4ba523029"),
                    OpisPrioriteta = "drugi testni prioritet"
                }

            });
        }
        */
      
        public PrioritetModel CreatePrioritet(PrioritetModel prioritetmodel)
        {
         
            var prioritet = context.Add(prioritetmodel);
            return mapper.Map<PrioritetModel>(prioritet.Entity);
        }

        public void DeletePrioritet(Guid prioritetId)
        {

            // Prioriteti.Remove(Prioriteti.FirstOrDefault(e => e.PrioritetID == prioritetId)); 
            var prioritet = GetPrioritetById(prioritetId);
            context.Remove(prioritet);
        }

        public PrioritetModel GetPrioritetById(Guid prioritetId)
        {
            return context.prioriteti.FirstOrDefault(e => e.PrioritetID == prioritetId);
        }

        public List<PrioritetModel> GetPrioriteti()
        {
            return context.prioriteti.ToList();
         }

        public void UpdatePrioritet(PrioritetModel prioritetModel)
        {
           //
        }
    }
}
