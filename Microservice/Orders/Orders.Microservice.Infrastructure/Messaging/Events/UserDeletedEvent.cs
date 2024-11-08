using MediatR;

namespace Orders.Microservice.Application.Events
{
    public class UserDeletedEvent : INotification
    {
        public int UserId { get; set; }
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
    }
}
