﻿using Trainify.Me_Api.Application.Controllers.Shared;
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
        [RequestSizeLimit(524288000)]
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

        [HttpGet("excluir/{aulaId}")]
        [Authorize(Roles = "Admin, Organizacao, Treinador")]
        public async Task<ActionResult<ApiResponse<Aula>>> RemoverAula([FromRoute] int aulaId)
        {
            try
            {
                var aula = await Services.AulaService.RemoverAula(aulaId);
                var response = ApiResponse<Aula>.SuccessResponse(aula, "Aula removida com sucesso.");
                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                var response = ApiResponse<Aula>.FailureResponse(ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpGet("concluir")]
        [Authorize(Roles = "Aluno")]
        public async Task<ActionResult<ApiResponse<Aula>>> ConcluirAula([FromQuery] int cursoAndamentoId,[FromQuery] int indiceAulaAtual)
        {
            try
            {
                var cursoAtuailzado = await Services.AulaService.ConcluirAula(cursoAndamentoId, indiceAulaAtual);
                var clientMessage = "";
                if(cursoAtuailzado.StatusCurso == Domain.Enums.StatusCursoAndamento.Concluido)
                {
                    clientMessage = "Curso concluído com sucesso!";
                }
                else
                {
                    clientMessage = "Aula concluída com sucesso";
                }
                var response = ApiResponse<CursoEmAndamento>.SuccessResponse(cursoAtuailzado, clientMessage);
                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                var response = ApiResponse<CursoEmAndamento>.FailureResponse(ex.Message);
                return StatusCode(500, response);
            }
        }


    }
}
