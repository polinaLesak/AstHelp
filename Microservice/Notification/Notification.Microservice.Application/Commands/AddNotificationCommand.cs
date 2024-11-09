using MediatR;
using Notification.Microservice.Domain.Entities;
using NotificationEntity = Notification.Microservice.Domain.Entities.Notification;

namespace Orders.Microservice.Application.Commands
{
    public class AddNotificationCommand : IRequest<NotificationEntity>
    {
        public int UserId { get; set; }
        public string Title { get; set; } = "";
        public string Message { get; set; } = "";
        public NotificationType Type { get; set; }
    }
}
