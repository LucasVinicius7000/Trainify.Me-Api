using Microsoft.AspNetCore.Mvc;
using Trainify.Me_Api.Controllers.Shared;

namespace Trainify.Me_Api.Controllers
{
    public class UserController : BaseController<UserController>
    {
        
        public UserController(IServiceProvider serviceProvider) : base(serviceProvider) { }


        [HttpGet]
        public ApiResponse<string> Get()
        {
            return ApiResponse<string>.SucessResponse("UserController", "No data");   
        }
    }
}