using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trainify.Me_Api.Application.Controllers.Shared;
using Trainify.Me_Api.Application.Models.Requests;
using Trainify.Me_Api.Domain.DTOs;

namespace Trainify.Me_Api.Application.Controllers
{
    public class AuthController : BaseController<AuthController>
    {

        public AuthController(IServiceProvider serviceProvider) : base(serviceProvider) { }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<UsuarioLogadoDTO>>> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(loginRequest.Senha) || string.IsNullOrEmpty(loginRequest.Email))
                    throw new Exception("Um ou mais dados de login são inválidos, verifique e tente novamente.");

                var usuario = await UserManager.FindByEmailAsync(loginRequest.Email);

                if (usuario is null)
                    throw new Exception("Email não corresponde a um usuário cadastrado.");

                if (await UserManager.CheckPasswordAsync(usuario, loginRequest.Senha))
                {
                    var tokenData = await Services.TokenService.GetAccessToken(usuario.Id);

                    HttpContext.Response.Headers["Authorization"] = tokenData.Token;
                    HttpContext.Response.Cookies.Append("RefreshToken", tokenData.RefreshToken);

                    var perfil = await Services.PerfilService.GetPerfilByUserId(usuario.Id);
                    var role = await UserManager.GetRolesAsync(usuario);

                    var usuarioLogado = new UsuarioLogadoDTO()
                    {
                        Perfil = perfil,
                        Email = usuario.Email,
                        UserName = usuario.UserName,
                        IsActive = usuario.IsActive,
                        Token = tokenData.Token,
                        RefreshToken = tokenData.RefreshToken,
                        RefreshTokenExpiration = tokenData.ExpirationRefreshToken,
                        Role = role.First(),
                    };

                    var response = ApiResponse<UsuarioLogadoDTO>.SuccessResponse(usuarioLogado, "Usuário autenticado com sucesso.");
                    return StatusCode(200, response);
                }
                else
                {
                    var response = ApiResponse<UsuarioLogadoDTO>.FailureResponse("Autenticação falhou. Email ou senha incorretos.");
                    return StatusCode(401, response);
                }

            }
            catch (Exception ex)
            {
                var response = ApiResponse<UsuarioLogadoDTO>.FailureResponse(ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpGet("refresh-token")]
        public async Task<ActionResult<ApiResponse<TokenDataDTO>>> RefreshToken([FromQuery] string oldRefreshToken)
        {
            try
            {
                if(string.IsNullOrEmpty(oldRefreshToken))
                        throw new Exception("Refresh token enviado inválido.");

                var novoTokenData = await Services.TokenService.RefreshToken(oldRefreshToken);

                if (novoTokenData is null)
                    throw new Exception("Ocorreu um erro ao obter um novo token de autenticação.");

                var response = ApiResponse<TokenDataDTO>.SuccessResponse(novoTokenData, "Novo token de autenticação gerado com sucesso.");
                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                var response = ApiResponse<TokenDataDTO>.FailureResponse(ex.Message);
                return StatusCode(500, response);
            }
        }



    }
}
