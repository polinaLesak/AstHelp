using MediatR;

namespace Notification.Microservice.Infrastructure.Messaging.Events
{
    public class UserDeletedEvent : INotification
    {
        public int UserId { get; set; }
    }
}
