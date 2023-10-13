﻿using Trainify.Me_Api.Infra.Data.Context;
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
            await _context.SaveChangesAsync();
            return alunoCriado.Entity;
        }

        public async Task<Aluno> BuscarAlunoPorUserId(string userId)
        {
            return await _context.Alunos.FirstAsync(a => a.UserId == userId);
        }

    }
}
