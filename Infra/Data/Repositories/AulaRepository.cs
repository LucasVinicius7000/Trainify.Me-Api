using Trainify.Me_Api.Infra.Data.Context;
using Trainify.Me_Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Trainify.Me_Api.Infra.Data.Repositories
{
    public class AulaRepository
    {
        public readonly TrainifyMeDbContext _context;
        public AulaRepository(TrainifyMeDbContext context)
        {
            _context = context;
        }

        public async Task<Aula> BuscarAulaPorId(int aulaId)
        {
            var aula = await _context.Set<Aula>().
                Where(a => a.Id == aulaId).
                FirstAsync();
            return aula;
        }

        public async Task<List<Aula>> BuscarAulasPorCursoId(int cursoId)
        {
            var aulas = await _context.Set<Aula>().
                Where(a => a.CursoId == cursoId).
                Include(a => a.Atividade).
                ToListAsync();
            return aulas;
        }

        public async Task<List<Alternativa>> BuscarAlternativasPorAtividadeId(int atividadeId)
        {
            var alternativas = await _context.Set<Alternativa>()
                .Where(alt => alt.AtividadeId == atividadeId)
                .ToListAsync();
            return alternativas;
        }

        public async Task<Aula> CriarAula(Aula aula)
        {
            var aulaCriada = await _context.Set<Aula>().AddAsync(aula);
            await _context.SaveChangesAsync();
            return aulaCriada.Entity;
        }

        public async Task<Aula> AtualizarAula(Aula aula)
        {
            var aulaAtualizada = _context.Set<Aula>().Update(aula);
            await _context.SaveChangesAsync();
            return aulaAtualizada.Entity;
        }

        public async Task<Aula> RemoverAulaEAtualizarIndices(Aula aula)
        {
            var aulaRemovida = _context.Set<Aula>()
            .Remove(aula).Entity;

            await _context.SaveChangesAsync();

            var aulas = await BuscarAulasPorCursoId(aulaRemovida.CursoId);
            aulas = aulas.OrderBy(a => a.Indice).ToList();

            var novaIndexacao = 1;
            foreach(var aulaAtual in aulas)
            {
                aulaAtual.Indice = novaIndexacao;
                _context.Update(aulaAtual);
                novaIndexacao++;
            }
            await _context.SaveChangesAsync();

            return aulaRemovida;

        }

        public async Task<Atividade> CriarAtividade(Atividade atividade)
        {
            var atividadeCriada = await _context.Set<Atividade>().AddAsync(atividade);
            await _context.SaveChangesAsync();
            return atividadeCriada.Entity;
        }

        public async Task<Atividade> AtualizarAtividade(Atividade atividade)
        {
            var atividadeAtualizada = _context.Set<Atividade>().Update(atividade);
            await _context.SaveChangesAsync();
            return atividadeAtualizada.Entity;
        }

        public async Task<Alternativa> CriarAlternativa(Alternativa alternativa)
        {
            var alternativaCriada = await _context.Set<Alternativa>().AddAsync(alternativa);
            await _context.SaveChangesAsync();
            return alternativaCriada.Entity;
        }


    }
}
