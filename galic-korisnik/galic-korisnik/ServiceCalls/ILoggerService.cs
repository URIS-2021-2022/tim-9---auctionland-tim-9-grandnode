using galic_korisnik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace galic_korisnik.ServiceCalls
{
    public interface ILoggerService
    {
        void CreateMessage(Message message);
    }
}
