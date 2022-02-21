using JavnoNadmetanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.ServiceCalls
{
    public interface IKupacService
    {
        Task<KupacDto> GetNajboljegPonudjaca(Guid kupacId);
    }
}
