using Notification.Microservice.Domain.Repositories;
using Notification.Microservice.Infrastructure.Persistence;

namespace Notification.Microservice.Infrastructure.Repositories
{
    public class NotificationRepository : GenericRepository<Domain.Entities.Notification, Guid>, INotificationRepository
    {
        public NotificationRepository(EFDBContext context) : base(context) { }
    }
}
