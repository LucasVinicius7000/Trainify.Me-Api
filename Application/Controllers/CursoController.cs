using Trainify.Me_Api.Application.Controllers.Shared;
using Trainify.Me_Api.Application.Models.Requests;
using Trainify.Me_Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Trainify.Me_Api.Application.Controllers
{
    public class CursoController : BaseController<CursoController>
    {
        public CursoController(IServiceProvider serviceProvider) : base(serviceProvider) { }

        [HttpPost("criar")]
        [Authorize(Roles = "Admin,Organizacao")]
        public async Task<ActionResult<ApiResponse<Curso>>> CriarCurso([FromBody] CriarCursoRequest cursoRequest)
        {
            try
            {
                var cursoCriado = await Services.CursoService.CriarCurso(cursoRequest.ToCurso());
                return StatusCode(200, ApiResponse<Curso>.SuccessResponse(cursoCriado, "Curso " + cursoCriado.Nome + " criado com sucesso."));
            }
            catch (Exception ex)
            {
                var response = ApiResponse<Curso>.FailureResponse(ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpGet("buscar/{id}")]
        [Authorize(Roles = "Admin, Organizacao, Treinador, Aluno")]
        public async Task<ActionResult<ApiResponse<Curso>>> BuscarCurso([FromRoute] int id)
        {
            try
            {
                var curso = await Services.CursoService.BuscarCursoPorId(id);
                var response = ApiResponse<Curso>.SuccessResponse(curso, "Curso encontrado.");
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                var response = ApiResponse<Curso>.FailureResponse(ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpGet("organizacao/buscar/{organizacaoId}")]
        [Authorize(Roles = "Admin, Organizacao")]
        public async Task<ActionResult<ApiResponse<List<Curso>>>> BuscarCursoPorOrganizacaoId([FromRoute] int organizacaoId)
        {
            try
            {
                var curso = await Services.CursoService.BuscarCursoPorOrganizacaoId(organizacaoId);
                var response = ApiResponse<List<Curso>>.SuccessResponse(curso, "Cursos listados com sucesso.");
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                var response = ApiResponse<List<Curso>>.FailureResponse(ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpGet("ativar/{cursoId}")]
        [Authorize(Roles = "Admin,Organizacao")]
        public async Task<ActionResult<ApiResponse<Curso>>> AtivarCurso([FromRoute] int cursoId)
        {
            try
            {
                var cursoAtivado = await Services.CursoService.AtivarCursoExistente(cursoId);
                return StatusCode(200, ApiResponse<Curso>.SuccessResponse(cursoAtivado, "Curso " + cursoAtivado.Nome + " ativado com sucesso."));
            }
            catch (Exception ex)
            {
                var response = ApiResponse<Curso>.FailureResponse(ex.Message);
                return StatusCode(500, response);
            }
        }

    }
}
