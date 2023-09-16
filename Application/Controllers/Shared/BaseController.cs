using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Trainify.Me_Api.Domain.Entities;
using Trainify.Me_Api.Services;

namespace Trainify.Me_Api.Application.Controllers.Shared
{
    [Route("api/[controller]")]
    [ApiController]
   public class BaseController<T> : ControllerBase where T : class
   {
        protected IConfiguration Configuration { get; }
        protected UserManager<User> UserManager { get; }
        protected SignInManager<User> SignInManager { get; }
        protected IService Services { get; }
        protected Logger<T> Logger { get; }
        //protected IBlobStorageService BlobStorage { get; }


        public BaseController(IServiceProvider serviceProvider)
        {
            Configuration = serviceProvider.GetService<IConfiguration>();
            UserManager = serviceProvider.GetService<UserManager<User>>();
            SignInManager = serviceProvider.GetService<SignInManager<User>>();
            Services = serviceProvider.GetService<IService>();
            //BlobStorage = serviceProvider.GetService<IBlobStorageService>();
            Logger = serviceProvider.GetService<Logger<T>>();
        }

   }
    
}
