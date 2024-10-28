namespace Notification.Microservice.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {

        Task<int> CommitAsync();
    }
}
