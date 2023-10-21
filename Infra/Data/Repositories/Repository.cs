using Trainify.Me_Api.Infra.Data.Context;


namespace Trainify.Me_Api.Infra.Data.Repositories
{
    public class Repository : IRepository
    {
        private readonly TrainifyMeDbContext _context;
        public Repository(TrainifyMeDbContext context)
        {
            _context = context;
        }

        //private readonly UserRepository? _user;
        //public UserRepository User
        //{
        //    get { return _user ?? new UserRepository(_context); }
        //}

        private readonly AlunoRepository _aluno;
        public AlunoRepository Aluno
        {
            get { return _aluno ?? new AlunoRepository(_context); }
        }

        private readonly OrganizacaoRepository _organizacao;
        public OrganizacaoRepository Organizacao
        {
            get { return _organizacao ?? new OrganizacaoRepository(_context); }
        }

        private readonly TreinadorRepository _treinador;
        public TreinadorRepository Treinador
        {
            get { return _treinador ?? new TreinadorRepository(_context); }
        }

        private readonly CursoRepository _curso;
        public CursoRepository Curso
        {
            get { return _curso ?? new CursoRepository(_context); }
        }

        private readonly AulaRepository _aula;
        public AulaRepository Aula
        {
            get { return _aula ?? new AulaRepository(_context); }
        }




        public async Task BeginTransaction()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task RollBackTransaction()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public async Task CommitTransaction()
        {
            await _context.Database.CommitTransactionAsync();
        }
    }
}
