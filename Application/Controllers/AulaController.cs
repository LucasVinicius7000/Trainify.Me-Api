using Trainify.Me_Api.Application.Controllers.Shared;
using Trainify.Me_Api.Application.Models.Requests;
using Trainify.Me_Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;



namespace Trainify.Me_Api.Application.Controllers
{
    public class AulaController : BaseController<AulaController>
    {
        public AulaController(IServiceProvider serviceProvider) : base(serviceProvider) { }


        [HttpPost("criar")]
        [Authorize(Roles = "Admin, Organizacao, Treinador")]
        public async Task<ActionResult<ApiResponse<Aula>>> AdicionarAula([FromBody] AdicionarAulaRequest aulaRequest)
        {
            try
            {
                var aula = await Services.AulaService.AdicionarAula(aulaRequest);
                var response = ApiResponse<Aula>.SuccessResponse(aula, "Aula criada com sucesso.");
                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                var response = ApiResponse<Aula>.FailureResponse(ex.Message);
                return StatusCode(500, response);
            }
        }


    }
}
