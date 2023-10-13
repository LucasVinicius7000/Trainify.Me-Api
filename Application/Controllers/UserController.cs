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

        [HttpPost("criar")]
        [Authorize(Roles = "Admin, Organizacao")]
        public async Task<ActionResult<ApiResponse<string>>> CriarUsuario(CriarUsuarioRequest criarUsuarioRequest) 
        {
            try
            {
                if (User.IsInRole("Organizacao"))
                {
                    if (criarUsuarioRequest.Role == "Organizacao" || criarUsuarioRequest.Role == "Admin")
                    {
                        var response = ApiResponse<User>.FailureResponse("Voc� n�o possui autoriza��o para criar esse tipo de usu�rio.", new[] { "Usu�rio com Role de Organiza��o n�o pode criar outros usu�rios com a mesma role ou com Role de Administrador." });
                        return StatusCode(403, response);
                    }
                }

                var usuarioCriado = await Services.PerfilService.CriarPerfilUsuario(criarUsuarioRequest);
                if (usuarioCriado is null) throw new Exception("Houve um erro ao criar usu�rio.");
                var responseSuccess = ApiResponse<dynamic>.SuccessResponse(usuarioCriado, "Usu�rio criado com sucesso.");
                return StatusCode(200, responseSuccess);

            }
            catch (Exception ex)
            {
                var response = ApiResponse<User>.FailureResponse(ex.Message);
                return StatusCode(500, response);
            }
        }

    }
}