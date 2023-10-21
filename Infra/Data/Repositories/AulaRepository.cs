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
