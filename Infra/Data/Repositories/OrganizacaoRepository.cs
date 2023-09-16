using Trainify.Me_Api.Infra.Data.Context;
using Trainify.Me_Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Trainify.Me_Api.Infra.Data.Repositories
{
    public class OrganizacaoRepository
    {
        public readonly TrainifyMeDbContext _context;
        public OrganizacaoRepository(TrainifyMeDbContext context)
        {
            _context = context;
        }

        public async Task<Organizacao> BuscarOrganizacaoPorUserId(string userId)
        {
            return await _context.Organizacoes.FirstAsync(a => a.UserId == userId);
        }
    }
}
