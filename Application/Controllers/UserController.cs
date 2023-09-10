using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trainify.Me_Api.Application.Controllers.Shared;
using Trainify.Me_Api.Application.Models.Requests;
using Trainify.Me_Api.Domain.Entities;

namespace Trainify.Me_Api.Application.Controllers
{
    public class UserController : BaseController<UserController>
    {
        public UserController(IServiceProvider serviceProvider) : base(serviceProvider) { }

        [HttpPost]
        //[Authorize(Roles = "Admin, Organizacao")]
        public async Task<ActionResult<ApiResponse<string>>> CriarUsuario(CriarUsuarioRequest criarUsuarioRequest) 
        {
            try
            {
                if (User.IsInRole("Organizacao"))
                {
                    if (criarUsuarioRequest.Role == "Organizacao" || criarUsuarioRequest.Role == "Admin")
                    {
                        Erros.Add("Usuário com Role de Organização não pode criar outros usuários com a mesma role ou com Role de Administrador.");
                        var response = ApiResponse<User>.FailureResponse("Você não possui autorização para criar esse tipo de usuário.", Erros);
                        return StatusCode(403, response);
                    }
                }
                     
            }
            catch (Exception ex)
            {
                var response = ApiResponse<User>.FailureResponse(ex.Message, Erros);
                return StatusCode(500, response);
            }
            return StatusCode(200, ApiResponse<User>.SuccessResponse(new User(), "0"));
        }

    }
}