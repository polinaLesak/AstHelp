namespace Notification.Microservice.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        INotificationRepository Notification { get; }

        Task<int> CommitAsync();
    }
}
