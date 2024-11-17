using MediatR;
using Orders.Microservice.Infrastructure.Messaging.Events;

namespace Orders.Microservice.Application.Events
{
    public class AddNotificationEvent : INotification
    {
        public int UserId { get; set; }
        public string Title { get; set; } = "";
        public string Message { get; set; } = "";
        public NotificationType Type { get; set; }
    }
}
