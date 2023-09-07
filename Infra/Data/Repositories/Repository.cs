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
