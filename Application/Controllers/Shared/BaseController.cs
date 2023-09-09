using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Trainify.Me_Api.Domain.Entities;

namespace Trainify.Me_Api.Application.Controllers.Shared
{
    [Route("api/[controller]/[action]")]
    [ApiController]
   public class BaseController<T> : ControllerBase where T : class
   {
        protected IConfiguration Configuration { get; }
        protected UserManager<User> UserManager { get; }
        protected SignInManager<User> SignInManager { get; }
        //protected IServicesLayer Services { get; }
        protected Logger<T> Logger { get; }
        //protected IBlobStorageService BlobStorage { get; }
        public List<dynamic> Erros { get; set; } = new List<dynamic>();


        public BaseController(IServiceProvider serviceProvider)
        {
            Configuration = serviceProvider.GetService<IConfiguration>();
            UserManager = serviceProvider.GetService<UserManager<User>>();
            SignInManager = serviceProvider.GetService<SignInManager<User>>();
            //Services = serviceProvider.GetService<IServicesLayer>();
            //BlobStorage = serviceProvider.GetService<IBlobStorageService>();
            Logger = serviceProvider.GetService<Logger<T>>();
        }

   }
    
}
