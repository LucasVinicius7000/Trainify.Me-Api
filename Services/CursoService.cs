using Trainify.Me_Api.Infra.Data.Repositories;
using Trainify.Me_Api.Domain.Entities;

namespace Trainify.Me_Api.Services
{
    public class CursoService
    {
        private readonly IService _services;
        private readonly IRepository _repositories;

        public CursoService(IService services, IRepository repositories)
        {
            _repositories = repositories;
            _services = services;
        }

        public async Task<Curso> CriarCurso(Curso curso)
        {
            try
            {
                await _repositories.BeginTransaction();
                if (string.IsNullOrEmpty(curso.Nome)) throw new Exception("Nome do curso inválido.");
                if (curso.OrganizacaoId <= 0) throw new Exception("O id da organização do curso é inválido.");
                if (string.IsNullOrEmpty(curso.UsuarioCriadorId)) throw new Exception("O id da usuário criador é invalido.");

                var cursoCriado = await _repositories.Curso.CriarCurso(curso);
                if (cursoCriado is null) throw new Exception("Ocorreu um desconhecido ao criar o curso.");
                await _repositories.CommitTransaction();
                return cursoCriado;

            }
            catch (Exception ex)
            {
                await _repositories.RollBackTransaction();
                throw new Exception(ex.Message);
            }
        }
    }
}
