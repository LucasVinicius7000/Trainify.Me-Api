using Trainify.Me_Api.Infra.Data.Context;
using Trainify.Me_Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Trainify.Me_Api.Infra.Data.Repositories
{
    public class AlunoRepository
    {
        public readonly TrainifyMeDbContext _context;
        public AlunoRepository(TrainifyMeDbContext context)
        {
            _context = context;
        }

        public async Task<Aluno> CriarAluno(Aluno aluno)
        {
            var alunoCriado = await _context.Set<Aluno>().AddAsync(aluno);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return alunoCriado.Entity;
        }

        public async Task<Aluno> BuscarAlunoPorId(int id)
        {
            return await _context.Alunos.Where(a => a.Id == id).FirstAsync();
        }

        public async Task<Aluno> BuscarAlunoPorUserId(string userId)
        {
            return await _context.Alunos.FirstAsync(a => a.UserId == userId);
        }

        public async Task<List<Aluno>> BuscarAlunoPorOrganizacaoId(int organizacaoId)
        {
            return await _context.Alunos.Where(a => a.OrganizacaoId == organizacaoId).ToListAsync();
        }

    }
}
