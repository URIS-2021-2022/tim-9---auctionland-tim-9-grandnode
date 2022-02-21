using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uplata.Models;

namespace Uplata.ServiceCalls
{
    public interface IJavnoNadmetanjeService
    {
        Task<JavnoNadmetanjeDto> GetJavnaNadmetanja(Guid javnoNadmetanjeID);
    }
}
