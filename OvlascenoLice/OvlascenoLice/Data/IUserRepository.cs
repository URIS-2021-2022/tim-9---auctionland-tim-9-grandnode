using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvlascenoLice.Data
{
   public interface IUserRepository
    {
        public bool UserWithCredentialsExists(string username, string password);
    }
}
