using Kupac_SK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.Data
{
   public interface IPrioritetRepository
    {
        List<PrioritetModel> GetPrioriteti();

        PrioritetModel GetPrioritetById(Guid prioritetId);

        PrioritetModel CreatePrioritet(PrioritetModel prioritetmodel);
       void UpdatePrioritet(PrioritetModel prioritetModel);

        void DeletePrioritet(Guid prioritetId);


    }
}
