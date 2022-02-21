using Kupac_SK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.ServiceCalls_
{
    public interface IovlascenoliceService
    {
        public Task <OvlascenoLiceDTO> GetOvlascenoLiceById(Guid? OLiceID);
    }
}
