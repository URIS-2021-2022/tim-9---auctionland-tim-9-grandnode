using Kupac_SK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.ServiceCalls_
{
    public interface IUplataService
    {
        public Task<UplataDTO> GetUplataById(Guid UplataID);

    }
}
