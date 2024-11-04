using Notification.Microservice.Domain.Repositories;

namespace Notification.Microservice.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFDBContext _context;
        public INotificationRepository Notification { get; }

        public UnitOfWork(
            EFDBContext context
            , INotificationRepository notification)
        {
            _context = context;
            Notification = notification;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
