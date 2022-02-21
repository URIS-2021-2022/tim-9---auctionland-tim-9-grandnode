using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorService.Models;

namespace UgovorService.ServiceCalls
{
    public interface IKupac_SKService
    {

        public Task<KupacDto> GetKupacById(Guid kupadId);
    }
}
