using MediatR;

namespace Orders.Microservice.Application.Commands
{
    public class DeleteAllNotificationsByUserIdCommand : IRequest
    {
        public int UserId { get; set; }

        public DeleteAllNotificationsByUserIdCommand(int userId)
        {
            UserId = userId;
        }
    }
}
