using MediatR;

namespace Orders.Microservice.Application.Commands
{
    public class MarkAllNotificationsAsReadByUserIdCommand : IRequest
    {
        public int UserId { get; set; }

        public MarkAllNotificationsAsReadByUserIdCommand(int userId)
        {
            UserId = userId;
        }
    }
}
