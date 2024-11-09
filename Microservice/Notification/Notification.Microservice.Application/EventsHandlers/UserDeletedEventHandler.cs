using MediatR;
using Notification.Microservice.Application.Events;
using Notification.Microservice.Domain.Repositories;
using NotificationEntity = Notification.Microservice.Domain.Entities.Notification;

namespace Notification.Microservice.Application.EventsHandlers
{
    public class UserDeletedEventHandler : INotificationHandler<UserDeletedEvent>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserDeletedEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UserDeletedEvent request, CancellationToken cancellationToken)
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
