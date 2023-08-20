using Microsoft.AspNetCore.Mvc;
using Trainify.Me_Api.Application.Controllers.Shared;

namespace Trainify.Me_Api.Application.Controllers
{
    public class UserController : BaseController<UserController>
    {
        
        public UserController(IServiceProvider serviceProvider) : base(serviceProvider) { }


        [HttpGet]
        public ApiResponse<string> Login()
        {
            return ApiResponse<string>.SucessResponse("UserController", "No data");   
        }
    }
}