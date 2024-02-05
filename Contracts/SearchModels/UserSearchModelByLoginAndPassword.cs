using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.SearchModels
{
    public class UserSearchModelByLoginAndPassword
    {
        public UserSearchModelByLoginAndPassword(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public string? Login { get; set; }
        public string? Password { get; set; }
    }
}
