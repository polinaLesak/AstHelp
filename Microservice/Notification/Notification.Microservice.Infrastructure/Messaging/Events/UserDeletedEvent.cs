using MediatR;

namespace Notification.Microservice.Application.Events
{
    public class UserDeletedEvent : INotification
    {
        public int UserId { get; set; }
    }
}
