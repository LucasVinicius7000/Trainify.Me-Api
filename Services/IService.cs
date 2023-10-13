using Microsoft.AspNetCore.Identity;
using Trainify.Me_Api.Domain.Entities;

namespace Trainify.Me_Api.Services
{
    public interface IService
    {
        IConfiguration Configuration { get; }
        UserManager<User> UserManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }
        SignInManager<User> SignInManager { get; }
        //UserServices UserServices { get; }
        //AuthServices AuthServices { get; }
        TokenService TokenService { get; }
        PerfilService PerfilService { get; }
        CursoService CursoService { get; }
    }
}
