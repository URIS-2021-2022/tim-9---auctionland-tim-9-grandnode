using Kupac_SK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.Data
{
    public class PrioritetRepository : IPrioritetRepository
    {
        public static List<PrioritetModel> Prioriteti { get; set; } = new List<PrioritetModel>();

        public PrioritetRepository()
        {
            FillData();
        }

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

      
        public PrioritetModel CreatePrioritet(PrioritetModel prioritetmodel)
        {

            prioritetmodel.PrioritetID = Guid.NewGuid(); //kreiramo novi kljuc 
            Prioriteti.Add(prioritetmodel);
            PrioritetModel prioritet = GetPrioritetById(prioritetmodel.PrioritetID);

            return new PrioritetModel //OVDE PAZI JER NEMAS CONFIRMATION 
            {
                PrioritetID = prioritet.PrioritetID,
                OpisPrioriteta = prioritet.OpisPrioriteta
             };
        }

        public void DeletePrioritet(Guid prioritetId)
        {
       
            Prioriteti.Remove(Prioriteti.FirstOrDefault(e => e.PrioritetID == prioritetId)); 
        }

        public PrioritetModel GetPrioritetById(Guid prioritetId)
        {
            return Prioriteti.FirstOrDefault(e => e.PrioritetID == prioritetId);
        }

        public List<PrioritetModel> GetPrioriteti()
        {
            return Prioriteti.ToList();
         }

        public void UpdatePrioritet(PrioritetModel prioritetModel)
        {
           //
        }
    }
}
