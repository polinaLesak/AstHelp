using MediatR;
using Notification.Microservice.Domain.Repositories;
using Orders.Microservice.Application.Commands;
using NotificationEntity = Notification.Microservice.Domain.Entities.Notification;

namespace Notification.Microservice.Application.Handlers
{
    public class DeleteAllNotificationsByUserIdCommandHandler
        : IRequestHandler<DeleteAllNotificationsByUserIdCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAllNotificationsByUserIdCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(
            DeleteAllNotificationsByUserIdCommand request,
            CancellationToken cancellationToken)
        {
            List<NotificationEntity> notifications = await _unitOfWork.Notification.GetAllNotificationsByUserId(request.UserId);
            notifications.ForEach(x =>
            {
                x.IsRead = true;
                _unitOfWork.Notification.Remove(x);
            });
            await _unitOfWork.CommitAsync();
        }
    }
}
