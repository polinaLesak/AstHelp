using MediatR;
using NotificationEntity = Notification.Microservice.Domain.Entities.Notification;

namespace Notifications.Microservice.Application.Queries
{
    public class GetAllNotificationsByUserIdQuery : IRequest<IEnumerable<NotificationEntity>>
    {

    }
}
