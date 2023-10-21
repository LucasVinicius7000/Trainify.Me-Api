namespace Trainify.Me_Api.Infra.Data.Repositories
{
    public interface IRepository
    {
        Task BeginTransaction();
        Task RollBackTransaction();
        Task CommitTransaction();
        AlunoRepository Aluno { get; }
        OrganizacaoRepository Organizacao { get; }
        TreinadorRepository Treinador { get; }
        CursoRepository Curso { get; }
        AulaRepository Aula { get; }

    }
}
