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

        [HttpPost]
        public async Task<ActionResult<ApiResponse<UsuarioLogadoDTO>>> Login([FromBody] LoginRequest loginRequest)
        {
            return StatusCode(200, new UsuarioLogadoDTO());
        }
        
    }
}
