using Contracts.BindingModels;
using Contracts.SearchModels;
using Contracts.StoragesContracts;
using Contracts.ViewModels;
using DataModels;
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

        private bool IsValidLogin(string login)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(login, @"^[a-zA-Z0-9]+$");
        }

        private bool IsValidPassword(string password)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(password, @"^[a-zA-Z0-9]+$");
        }

        private bool IsValidName(string name)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(name, @"^[a-zA-Zа-яА-Я]+$");
        }
        public void Create(CreateUserBindingModel model, AuthData data)
        {
            if (_source.Users.Any(u => u.Login == model.Login) || !IsValidLogin(model.Login) || !IsValidPassword(model.Password) || !IsValidName(model.Name))
            {
                return;
            }

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

        public List<UserViewModel> GetUsersOverAge(AuthData data, int age)
        {
            if (isAdmin(new UserSearchModelByLoginAndPassword(data.LoginAuth, data.PasswordAuth)))
            {
                DateTime currentDate = DateTime.Now;
                return GetUsers()
                    .Where(x => x.Birthday != null && (currentDate - x.Birthday.Value).TotalDays / 365 > age)
                    .ToList();
            }
            else
            {
                return null;
            }
        }

        public UserViewModel GetUserYourself(AuthData dataAuth, string login, string password)
        {
            if (dataAuth.LoginAuth == login && dataAuth.PasswordAuth == password)
            {
                User user = getUserByLogin(login);
                if (user != null && dataAuth.PasswordAuth == user.Password && user.RevokedOn == null)
                {
                    return user.GetViewModel;
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

        public void RecoveryUser(AuthData data, string Login)
        {
            if (isAdmin(new UserSearchModelByLoginAndPassword(data.LoginAuth, data.PasswordAuth)))
            {
                User user = getUserByLogin(Login);
                if(user != null && user.RevokedOn != null && user.RevokedBy != null)
                {
                    user.RevokedOn = null;
                    user.RevokedBy = null;
                }
            }
        }

        public void UpdateNameGenderDate(AuthData data, Guid id, string Name, Gender Gender, DateTime Birthday)
        {
            User user = _source.Users.FirstOrDefault(x => x.Guid == id);
            if (user != null)
            {
                if (isAdmin(new UserSearchModelByLoginAndPassword(data.LoginAuth, data.PasswordAuth)) || user.Login == data.LoginAuth)
                {
                    if(user.RevokedOn == null)
                    {
                        user.Name = Name;
                        user.Gender = Gender;
                        user.Birthday = Birthday;
                        user.ModifiedOn = DateTime.Now;
                        user.ModifiedBy = data.LoginAuth;
                    }
                }

            }
        }

        public void UpdateLogin(AuthData data, Guid id, string Login)
        {
            User user = _source.Users.FirstOrDefault(x => x.Guid == id);
            if (user != null)
            {
                if (isAdmin(new UserSearchModelByLoginAndPassword(data.LoginAuth, data.PasswordAuth)) || user.Login == data.LoginAuth)
                {
                    if (_source.Users.All(u => u.Login != Login))
                    {
                        if (user.RevokedOn == null)
                        {
                            user.ModifiedOn = DateTime.Now;
                            user.ModifiedBy = data.LoginAuth;
                            user.Login = Login;
                        }
                    }               
                }
            }
        }

        public void UpdatePassword(AuthData data, Guid id, string Password)
        {
            User user = _source.Users.FirstOrDefault(x => x.Guid == id);
            if (user != null)
            {
                if (isAdmin(new UserSearchModelByLoginAndPassword(data.LoginAuth, data.PasswordAuth)) || user.Login == data.LoginAuth)
                {
                    if (user.RevokedOn == null)
                    {
                        user.ModifiedOn = DateTime.Now;
                        user.ModifiedBy = data.LoginAuth;
                        user.Password = Password;
                    }
                }
            }
        }
    }
}
