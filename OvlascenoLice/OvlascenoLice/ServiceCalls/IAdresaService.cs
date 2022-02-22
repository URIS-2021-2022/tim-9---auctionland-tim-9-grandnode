using OvlascenoLice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvlascenoLice.ServiceCalls
{
    public interface IAdresaService
    {
        public Task<AdresaDto> GetAdresaById(Guid AdresaId);
    }
}
