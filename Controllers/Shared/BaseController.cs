using Microsoft.AspNetCore.Mvc;

namespace Trainify.Me_Api.Controllers.Shared
{
    [Route("api/[controller]/[action]")]
    [ApiController]
   public class BaseController<T> : ControllerBase where T : class
   {
        protected IConfiguration Configuration { get; }
        //protected UserManager<IdentityUser> UserManager { get; }
        //protected SignInManager<IdentityUser> SignInManager { get; }
        //protected IServicesLayer Services { get; }
        protected Logger<T> Logger { get; }
        //protected IBlobStorageService BlobStorage { get; }



        public BaseController(IServiceProvider serviceProvider)
        {
            Configuration = serviceProvider.GetService<IConfiguration>();
            //UserManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            //SignInManager = serviceProvider.GetService<SignInManager<IdentityUser>>();
            //Services = serviceProvider.GetService<IServicesLayer>();
            //BlobStorage = serviceProvider.GetService<IBlobStorageService>();
            Logger = serviceProvider.GetService<Logger<T>>();
        }

   }
    
}
