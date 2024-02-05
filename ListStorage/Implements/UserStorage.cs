using Contracts.BindingModels;
using Contracts.SearchModels;
using Contracts.StoragesContracts;
using Contracts.ViewModels;
using ListStorage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListStorage.Implements
{
    public class UserStorage : IUserStorage
    {
        private readonly DataListSingleton _source;

        public UserStorage()
        {
            _source = DataListSingleton.GetInstance();
        }

        public List<UserViewModel> GetUsers()
        {
            var result = new List<UserViewModel>();
            foreach (var user in _source.Users)
            {
                result.Add(user.GetViewModel);
            }
            return result;
        }

        private bool isAdmin(UserSearchModelByLoginAndPassword model)
        {
            UserViewModel? user = _source.Users.FirstOrDefault(x => x.Login == model.Login)?.GetViewModel;
            if (user != null && user.Password == model.Password && user.Admin == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private User? getUserByLogin(string Login)
        {
            return _source.Users.FirstOrDefault(x => x.Login == Login);
        }
        public void Create(CreateUserBindingModel model, AuthData data)
        {
            User user = new User(
                Guid.NewGuid(),
                model.Login,
                model.Password,
                model.Name,
                model.Gender,
                model.Birthday,
                isAdmin(new UserSearchModelByLoginAndPassword(data.LoginAuth, data.PasswordAuth)) ? model.Admin : false,
                DateTime.Now,
                data.LoginAuth,
                DateTime.Now,
                data.LoginAuth,
                null,
                null
                );
            _source.Users.Add(user);
        }

        public void SoftDelete(AuthData data, string Login)
        {
            if (isAdmin(new UserSearchModelByLoginAndPassword(data.LoginAuth, data.PasswordAuth)))
            {
                User? user = getUserByLogin(Login);
                if (user != null)
                {
                    user.RevokedOn = DateTime.Now;
                    user.RevokedBy = data.LoginAuth;
                }
            }
        }

        public void HardDelete(AuthData data, string Login)
        {
            if (isAdmin(new UserSearchModelByLoginAndPassword(data.LoginAuth, data.PasswordAuth)))
            {
                User? user = getUserByLogin(Login);
                if (user != null)
                {
                    _source.Users.Remove(user);
                }
            }
        }

        public List<UserViewModel> GetActiveUserOrderByAsc(AuthData data)
        {
            if (isAdmin(new UserSearchModelByLoginAndPassword(data.LoginAuth, data.PasswordAuth)))
            {
                return GetUsers().Where(x => x.RevokedBy == null).OrderBy(x => x.CreatedOn).ToList();
            }
            else
            {
                return null;
            }

        }

        public UserViewModelWithActiveStatus GetUserDataWithStatus(AuthData data, string Login)
        {
            if (isAdmin(new UserSearchModelByLoginAndPassword(data.LoginAuth, data.PasswordAuth)))
            {
                User? user = getUserByLogin(Login);
                if (user != null)
                {
                    return new UserViewModelWithActiveStatus(
                        user.Name,
                        user.Gender,
                        user.Birthday,
                        user.RevokedBy == null ? true : false
                    );
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public List<UserViewModel> GetUsersOverAge(AuthData data, int Age)
        {
            if (isAdmin(new UserSearchModelByLoginAndPassword(data.LoginAuth, data.PasswordAuth)))
            {
                return (List<UserViewModel>)GetUsers().Where(x => (DateTime.Now.Year - x.Birthday?.Year) > Age);
            }
            else
            {
                return null;
            }
        }
    }
}
