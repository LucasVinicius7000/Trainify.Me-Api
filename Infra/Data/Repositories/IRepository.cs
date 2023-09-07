namespace Trainify.Me_Api.Infra.Data.Repositories
{
    public interface IRepository
    {
        Task BeginTransaction();
        Task RollBackTransaction();
        Task CommitTransaction();
    }
}
