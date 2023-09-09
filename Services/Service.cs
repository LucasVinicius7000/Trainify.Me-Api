using Microsoft.AspNetCore.Identity;
using Trainify.Me_Api.Infra.Data.Repositories;
using Trainify.Me_Api.Domain.Entities;

namespace Trainify.Me_Api.Services
{
    public class Service : IService
    {

        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public IConfiguration Configuration => _configuration;
        public UserManager<User> UserManager => _userManager;
        public RoleManager<IdentityRole> RoleManager => _roleManager;
        public SignInManager<User> SignInManager => _signInManager;
        public UserServices UserServices { get; private set; }

        public Service
        (
            IConfiguration configuration,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager,
            IRepository repositories
        )
        {
            this._configuration = configuration;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._signInManager = signInManager;
            this.UserServices = UserServices ?? new UserServices(this, repositories);
        }
    }
}
