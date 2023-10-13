using Trainify.Me_Api.Infra.Data.Context;
using Trainify.Me_Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Trainify.Me_Api.Infra.Data.Repositories
{
    public class TreinadorRepository
    {
        public readonly TrainifyMeDbContext _context;
        public TreinadorRepository(TrainifyMeDbContext context)
        {
            _context = context;
        }

        public async Task<Treinador> CriarTreinador(Treinador treinador)
        {
            var treinadorCriado = await _context.Set<Treinador>().AddAsync(treinador);
            await _context.SaveChangesAsync();
            return treinadorCriado.Entity;
        }

        public async Task<Treinador> BuscarTreinadorPorUserId(string userId)
        {
            return await _context.Treinadores.FirstAsync(a => a.UserId == userId);
        }
    }
}
