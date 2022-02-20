using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZalbaService.Models;

namespace ZalbaService.ServiceCalls
{
    public interface ILoggerService
    {
        void CreateMessage(Message message);
    }
}
