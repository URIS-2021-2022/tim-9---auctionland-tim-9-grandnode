using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZalbaService.Models;

namespace ZalbaService.ServiceCalls
{
    interface IKupacService
    {
        public bool PodnosenjeZalbe(KupacDto kupac);
    }
}
