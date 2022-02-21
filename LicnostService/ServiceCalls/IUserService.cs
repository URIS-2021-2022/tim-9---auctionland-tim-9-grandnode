using LicnostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicnostService.ServiceCalls
{
    interface IUserService
    {
        public bool validateUser(Principal principal);
    }
}
