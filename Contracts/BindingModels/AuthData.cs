using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.BindingModels
{
    public class AuthData
    {
        public string LoginAuth {  get; set; } = string.Empty;
        public string PasswordAuth { get; set; } = string.Empty;
    }
}
