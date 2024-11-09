using MediatR;

namespace Orders.Microservice.Application.Commands
{
    public class MarkNotificationAsReadByIdCommand : IRequest
    {
        public Guid NotificationId { get; set; }

        public MarkNotificationAsReadByIdCommand(Guid notificationId)
        {
            NotificationId = notificationId;
        }
    }
}
