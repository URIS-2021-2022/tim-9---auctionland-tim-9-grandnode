using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorService.Models;

namespace UgovorService.ServiceCalls
{
    public interface IJavnoNadmetanjeService
    {
        public Task<JavnoNadmetanjeDto> GetJavnoNadmetanjeByID(Guid javnoNadmetanjeID);
    }
}
