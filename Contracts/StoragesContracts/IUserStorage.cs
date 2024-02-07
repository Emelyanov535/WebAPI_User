using Contracts.BindingModels;
using Contracts.SearchModels;
using Contracts.ViewModels;
using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.StoragesContracts
{
    public interface IUserStorage
    {
        List<UserViewModel> GetUsers();
        void Create(CreateUserBindingModel model, AuthData data);
        void SoftDelete(AuthData data, string Login);
        void HardDelete(AuthData data, string Login);
        List<UserViewModel> GetActiveUserOrderByAsc(AuthData data);
        UserViewModelWithActiveStatus GetUserDataWithStatus(AuthData data, string Login);
        UserViewModel GetUserYourself(AuthData dataAuth, string login, string password);
        List<UserViewModel> GetUsersOverAge(AuthData data, int Age);
        void RecoveryUser(AuthData data, string Login);
        void UpdateNameGenderDate(AuthData data, Guid id, string Name, Gender Gender, DateTime Birthday);
        void UpdateLogin(AuthData data, Guid id, string Login);
        void UpdatePassword(AuthData data, Guid id, string Password);
    }
}
