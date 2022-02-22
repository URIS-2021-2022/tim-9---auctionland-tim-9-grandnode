using Kupac_SK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.ServiceCalls_
{
    public interface IAdresaService
    {
        public Task<AdresaDto> GetAdresaById(Guid AdresaId);
    }
}
