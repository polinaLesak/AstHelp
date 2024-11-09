using Microsoft.EntityFrameworkCore;
using Notification.Microservice.Domain.Repositories;
using Notification.Microservice.Infrastructure.Persistence;
using NotificationEntity = Notification.Microservice.Domain.Entities.Notification;

namespace Notification.Microservice.Infrastructure.Repositories
{
    public class NotificationRepository : GenericRepository<NotificationEntity, Guid>, INotificationRepository
    {
        public NotificationRepository(EFDBContext context) : base(context) { }

        public async Task<List<NotificationEntity>> GetAllNotificationsByUserId(int userId)
        {
            return await _context.Notifications
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }
    }
}
