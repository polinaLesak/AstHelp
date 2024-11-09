using NotificationEntity = Notification.Microservice.Domain.Entities.Notification;

namespace Notification.Microservice.Domain.Repositories
{
    public interface INotificationRepository : IGenericRepository<NotificationEntity, Guid>
    {
        Task<List<NotificationEntity>> GetAllNotificationsByUserId(int userId);
    }
}
