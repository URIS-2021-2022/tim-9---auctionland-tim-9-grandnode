using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.ServiceCalls
{
    public interface IKupac_SKService
    {
        public Task<KupacDto> GetKupacById(Guid kupacId);
    }
}
