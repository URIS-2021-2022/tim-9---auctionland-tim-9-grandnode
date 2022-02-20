using LicnostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicnostService.ServiceCalls
{
    interface ILoggerService
    {
        void CreateMessage(Message message);
    }
}
