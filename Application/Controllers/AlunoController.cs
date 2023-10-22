using Microsoft.AspNetCore.Mvc;
using Trainify.Me_Api.Application.Controllers.Shared;
using Trainify.Me_Api.Application.Models.Requests;
using Trainify.Me_Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace Trainify.Me_Api.Application.Controllers
{
    public class AlunoController : BaseController<AlunoController>
    {
        public AlunoController(IServiceProvider serviceProvider) : base(serviceProvider) { }

        [HttpGet("listar/{organizacaoId}")]
        [Authorize(Roles = "Admin,Organizacao")]
        public async Task<ActionResult<ApiResponse<List<Aluno>>>> ListarAlunoPorOrganizacao([FromRoute] int organizacaoId)
        {
            try
            {
                var alunos = await Services.PerfilService.ListarAlunosPorOrganizacaoId(organizacaoId);
                return StatusCode(200, ApiResponse<List<Aluno>>.SuccessResponse(alunos, "Alunos listados com sucesso."));
            }
            catch (Exception ex)
            {
                var response = ApiResponse<List<Aluno>>.FailureResponse(ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpPost("matricular/{alunoId}/{cursoId}")]
        [Authorize(Roles = "Admin,Organizacao")]
        public async Task<ActionResult<ApiResponse<CursoEmAndamento>>> MatricularAlunoEmCurso([FromRoute] int alunoId, [FromRoute] int cursoId)
        {
            try
            {
                var cursoEmAndamento = await Services.CursoService.MatricularAlunoEmCurso(alunoId, cursoId);
                return StatusCode(200, ApiResponse<CursoEmAndamento>.SuccessResponse(cursoEmAndamento, "Alunos matriculado com sucesso."));
            }
            catch (Exception ex)
            {
                var response = ApiResponse<CursoEmAndamento>.FailureResponse(ex.Message);
                return StatusCode(500, response);
            }
        }
    }
}
