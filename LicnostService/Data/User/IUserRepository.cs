using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicnostService.Data.User
{
    public interface IUserRepository
    {
        public bool validateUser(string username, string password);
    }
}
