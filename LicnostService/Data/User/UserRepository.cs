using LicnostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicnostService.Data.User
{
    public class UserRepository : IUserRepository
    {
        public static List<Principal> Users { get; set; } = new List<Principal>();

        public UserRepository() 
        {
            FillData();
        }

        public bool validateUser(string username, string password)
        {
            Principal user = Users.FirstOrDefault(u => u.Username == username);

            if (user == null) 
            {
                return false;
            }

            if (user.Password == password) 
            {
                return true;
            }

            return false;
        }

        private void FillData() 
        {
            Users.AddRange(new List<Principal>
            {
                new Principal
                {
                    Username = "admin",
                    Password = "admin"
                }
            });
        }
    }
}
