using Contracts.BindingModels;
using Contracts.BusiniesLogicContracts;
using Contracts.ViewModels;
using DataModels;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IUserLogic _logic;

        public UserController(IUserLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        public List<UserViewModel> GetUsers()
        {
            return _logic.ReadList();
        }

        [HttpPost]
        public void CreateUser(AuthData data, CreateUserBindingModel model)
        {
            _logic.Create(model, data);
        }

        [HttpDelete]
        public void SoftDeleteUser(AuthData data, string Login)
        {
            _logic.SoftDelete(data, Login);
        }

        [HttpDelete]
        public void HardDeleteUser(AuthData data, string Login)
        {
            _logic.HardDelete(data, Login);
        }

        [HttpGet]
        public List<UserViewModel> GetActiveUserOrderByAsc(AuthData data)
        {
            return _logic.GetActiveUserOrderByAsc(data);
        }

        [HttpGet]
        public UserViewModelWithActiveStatus GetUserWithActiveStatus(AuthData data, string Login) {
            return _logic.GetUserDataWithStatus(data, Login);
        }

        [HttpGet]
        public List<UserViewModel> GetUsersOverAge(AuthData data, int Age)
        {
            return _logic.GetUsersOverAge(data, Age);
        }

        [HttpGet]
        public UserViewModel GetUserYorself(AuthData data, string login, string password)
        {
            return _logic.GetUserYourself(data, login, password);
        }

        [HttpPut]
        public void RecoveryUser(AuthData data, string Login)
        {
            _logic.RecoveryUser(data, Login);
        }

        [HttpPut]
        public void UpdateNameGenderDate(AuthData data, Guid id, string Name, Gender Gender, DateTime Birthday) 
        {
            _logic.UpdateNameGenderDate(data, id, Name, Gender, Birthday);
        }

        [HttpPut]
        public void UpdateLogin(AuthData data, Guid id, string Login)
        {
            _logic.UpdateLogin(data, id, Login);
        }

        [HttpPut]
        public void UpdatePassword(AuthData data, Guid id, string Password)
        {
            _logic.UpdateLogin(data, id, Password);
        }
    }
}
