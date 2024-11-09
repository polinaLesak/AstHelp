using MediatR;

namespace Orders.Microservice.Application.Commands
{
    public class DeleteNotificationByIdCommand : IRequest
    {
        public Guid NotificationId { get; set; }

        public DeleteNotificationByIdCommand(Guid notificationId)
        {
            NotificationId = notificationId;
        }
    }
}
