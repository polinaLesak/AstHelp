using Cart.Microservice.Infrastructure.Messaging.Events;
using MediatR;

namespace Cart.Microservice.Application.Events
{
    public class AddNotificationEvent : INotification
    {
        public int UserId { get; set; }
        public string Title { get; set; } = "";
        public string Message { get; set; } = "";
        public NotificationType Type { get; set; }
    }
}
