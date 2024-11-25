using MediatR;
using Notification.Microservice.Domain.Entities;

namespace Notification.Microservice.Infrastructure.Messaging.Events
{
    public class AddNotificationEvent : INotification
    {
        public int UserId { get; set; }
        public string Title { get; set; } = "";
        public string Message { get; set; } = "";
        public NotificationType Type { get; set; }
    }
}
