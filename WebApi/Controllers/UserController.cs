using Contracts.BindingModels;
using Contracts.BusiniesLogicContracts;
using Contracts.ViewModels;
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

    }
}
