using Contracts.BindingModels;
using Contracts.BusiniesLogicContracts;
using Contracts.SearchModels;
using Contracts.StoragesContracts;
using Contracts.ViewModels;
using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserStorage _userStorage;
        public UserLogic(IUserStorage userStorage)
        {
            _userStorage = userStorage;
        }

        public void Create(CreateUserBindingModel model, AuthData data)
        {
            _userStorage.Create(model, data);
        }

        public List<UserViewModel> GetActiveUserOrderByAsc(AuthData data)
        {
            return _userStorage.GetActiveUserOrderByAsc(data);
        }

        public UserViewModelWithActiveStatus GetUserDataWithStatus(AuthData data, string Login)
        {
            return _userStorage.GetUserDataWithStatus(data, Login);
        }

        public List<UserViewModel> GetUsersOverAge(AuthData data, int Age)
        {
            return _userStorage.GetUsersOverAge(data, Age);
        }

        public UserViewModel GetUserYourself(AuthData dataAuth, string login, string password)
        {
            return _userStorage.GetUserYourself(dataAuth, login, password);
        }

        public void HardDelete(AuthData data, string Login)
        {
            _userStorage.HardDelete(data, Login);
        }

        public List<UserViewModel>? ReadList()
        {
            return _userStorage.GetUsers();
        }

        public void RecoveryUser(AuthData data, string Login)
        {
            _userStorage.RecoveryUser(data, Login);
        }

        public void SoftDelete(AuthData data, string Login)
        {
            _userStorage.SoftDelete(data, Login);
        }

        public void UpdateLogin(AuthData data, Guid id, string Login)
        {
            _userStorage.UpdateLogin(data, id, Login);
        }

        public void UpdateNameGenderDate(AuthData data, Guid id, string Name, Gender Gender, DateTime Birthday)
        {
            _userStorage.UpdateNameGenderDate(data, id, Name, Gender, Birthday);
        }

        public void UpdatePassword(AuthData data, Guid id, string Password)
        {
            _userStorage.UpdatePassword(data, id, Password);
        }
    }
}
