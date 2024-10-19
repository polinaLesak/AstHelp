namespace Identity.Microservice.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IProfileRepository Profiles { get; }
        IRoleRepository Roles { get; }
        Task<int> CommitAsync();
    }
}
