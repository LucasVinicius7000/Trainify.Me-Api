﻿using Trainify.Me_Api.Infra.Data.Context;
using Trainify.Me_Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Trainify.Me_Api.Infra.Data.Repositories
{
    public class CursoRepository
    {
        public readonly TrainifyMeDbContext _context;
        public CursoRepository(TrainifyMeDbContext context)
        {
            _context = context;
        }

        #region Cursos

        public async Task<Curso> CriarCurso(Curso curso)
        {
            var cursoCriado = _context.Set<Curso>()
                .Add(curso);
            await _context.SaveChangesAsync();
            return cursoCriado.Entity;
        }

        public async Task<Curso> GetCursoById(int id)
        {
            try
            {
                 return await _context.Set<Curso>()
                .Where(c => c.Id == id)
                .Include(c => c.Aulas)
                .Include(c => c.OrganizacaoPertencente)
                //.Include(c => c.UsuarioCriador)
                .FirstAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Curso> AtivarCurso(int cursoId)
        {
            var curso = await _context.Set<Curso>()
                .Where(c => c.Id == cursoId)
                .FirstAsync();

            curso.Status = Domain.Enums.StatusCurso.Ativo;

            var cursoAtivado = _context.Set<Curso>()
                .Update(curso);

            await _context.SaveChangesAsync();
            return cursoAtivado.Entity;

        }

        public async Task<Curso> InativarCurso(int cursoId)
        {
            var curso = await _context.Set<Curso>()
                .Where(c => c.Id == cursoId)
                .FirstAsync();

            curso.Status = Domain.Enums.StatusCurso.Inativo;

            var cursoInativado = _context.Set<Curso>()
                .Update(curso);

            await _context.SaveChangesAsync();
            return cursoInativado.Entity;

        }

        public async Task<Curso> EditarCurso(Curso curso)
        {
            var cursoAtualizado = _context.Set<Curso>()
                .Update(curso);
            await _context.SaveChangesAsync();
            return cursoAtualizado.Entity;
        }

        public async Task<List<Curso>> ListaCursosPelaOrganizacao(int organizacaoId)
        {
            return await _context.Set<Curso>()
                .Where(c => c.OrganizacaoId == organizacaoId)
                .Include(c => c.Aulas)
                .ToListAsync();
        }

        #endregion


        #region CursoEmAndamento


        public async Task<CursoEmAndamento> CriarCursoEmAndamento(CursoEmAndamento curso)
        {
            try
            {
                var cursoCriado = _context.Set<CursoEmAndamento>()
                .Add(curso);
                await _context.SaveChangesAsync();
                return cursoCriado.Entity;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        
        public async Task<List<CursoEmAndamento>> ListaCursosEmAndamento(int alunoId)
        {
            return await _context.Set<CursoEmAndamento>()
                .Where(c => c.AlunoId == alunoId)
                .ToListAsync();
        }

        public async Task<CursoEmAndamento> BuscaCursoEmAndamentoById(int cursoEmAndamentoId)
        {
            return await _context.Set<CursoEmAndamento>()
                .Where(c => c.Id == cursoEmAndamentoId)
                .FirstAsync();
        }

        public async Task<List<CursoEmAndamento>> BuscaCursoEmAndamentoByAlunoId(int alunoId)
        {
            return await _context.Set<CursoEmAndamento>()
                .Where(
                    c =>
                        c.AlunoId == alunoId  
                 )
                .ToListAsync();
        }
        
        public async Task<CursoEmAndamento> ConcluirCurso(int cursoEmAndamentoId)
        {
            var curso = await _context.Set<CursoEmAndamento>()
                .Where(c => c.Id == cursoEmAndamentoId)
                .FirstAsync();

            curso.StatusCurso = Domain.Enums.StatusCursoAndamento.Concluido;

            var cursoAtualizado = _context.Set<CursoEmAndamento>()
                .Update(curso);

            await _context.SaveChangesAsync();
            return cursoAtualizado.Entity;
        }

        public async Task<CursoEmAndamento> AtualizarAulaCursoEmAndamento(int aulaIdConcluida, int cursoEmAndamentoId)
        {
            var aulaConcluida = await _context.Set<Aula>()
                .Where(a => a.Id == aulaIdConcluida)
                .FirstAsync();

            var indiceProximaAula = aulaConcluida.Indice + 1;
            var proximaAula = await _context.Set<Aula>()
                .Where(a => a.Indice == indiceProximaAula && a.CursoId == aulaConcluida.CursoId)
                .FirstAsync();

            var curso = await _context.Set<CursoEmAndamento>()
                .Where(c => c.Id == cursoEmAndamentoId)
                .FirstAsync();


            curso.AulaAtualId = proximaAula.Id;

            var cursoEmAndamento = _context.Set<CursoEmAndamento>()
                .Update(curso);

            await _context.SaveChangesAsync();
            return cursoEmAndamento.Entity;

        }

        public async Task<Curso> ExcluirCurso(int cursoId)
        {
            var curso = await this.GetCursoById(cursoId);
            if(curso == null) return null;
            var cursoRemovido = _context.Set<Curso>()
                .Remove(curso);
            await _context.SaveChangesAsync();
            return cursoRemovido.Entity;

        }
        
        #endregion  

    }
}
