using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Trainify.Me_Api.Application.Controllers.Shared;
using Trainify.Me_Api.Application.Models.Requests;
using Trainify.Me_Api.Domain.Entities;
using Trainify.Me_Api.Infra.Services.BlobStorage;

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
                        var response = ApiResponse<User>.FailureResponse("Você não possui autorização para criar esse tipo de usuário.", new[] { "Usuário com Role de Organização não pode criar outros usuários com a mesma role ou com Role de Administrador." });
                        return StatusCode(403, response);
                    }
                }

                var usuarioCriado = await Services.PerfilService.CriarPerfilUsuario(criarUsuarioRequest);
                if (usuarioCriado is null) throw new Exception("Houve um erro ao criar usuário.");
                var responseSuccess = ApiResponse<dynamic>.SuccessResponse(usuarioCriado, "Usuário criado com sucesso.");
                return StatusCode(200, responseSuccess);

            }
            catch (Exception ex)
            {
                var response = ApiResponse<User>.FailureResponse(ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> UploadFile([FromBody] UploadFileRequest fileRequest)
        {
            try
            {
                var data = new MemoryStream(Convert.FromBase64String(fileRequest.Base64Arquivo));
                var urlDoArquivo = await BlobStorage.UploadFile(fileRequest.NomeArquivo, fileRequest.ExtensaoArquivo, fileRequest.MimeType, data);
                return StatusCode(200, urlDoArquivo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}