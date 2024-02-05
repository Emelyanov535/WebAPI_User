using Contracts.BindingModels;
using Contracts.SearchModels;
using Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.BusiniesLogicContracts
{
    public interface IUserLogic
    {
        List<UserViewModel>? ReadList();
        void Create(CreateUserBindingModel model, AuthData data);

        void SoftDelete(AuthData data, string Login);
        void HardDelete(AuthData data, string Login);
    }
}
