namespace Identity.Microservice.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        Task<int> CommitAsync();
    }
}
