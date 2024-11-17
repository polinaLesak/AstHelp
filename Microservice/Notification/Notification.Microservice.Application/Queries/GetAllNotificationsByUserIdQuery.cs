using MediatR;
using NotificationEntity = Notification.Microservice.Domain.Entities.Notification;

namespace Notifications.Microservice.Application.Queries
{
    public class GetAllNotificationsByUserIdQuery : IRequest<IEnumerable<NotificationEntity>>
    {
        public int UserId { get; set; }

        public GetAllNotificationsByUserIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}
