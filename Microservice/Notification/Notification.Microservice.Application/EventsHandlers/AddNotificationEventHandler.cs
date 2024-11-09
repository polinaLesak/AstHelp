using MediatR;
using Notification.Microservice.Application.Events;
using Notification.Microservice.Domain.Repositories;
using NotificationEntity = Notification.Microservice.Domain.Entities.Notification;

namespace Notificationt.Microservice.Application.EventsHandlers
{
    public class AddNotificationEventHandler : INotificationHandler<AddNotificationEvent>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddNotificationEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(AddNotificationEvent notification, CancellationToken cancellationToken)
        {
            await _unitOfWork.Notification.AddAsync(new NotificationEntity
            {
                UserId = notification.UserId,
                Title = notification.Title,
                Message = notification.Message,
                Timestamp = DateTime.UtcNow,
                Type = notification.Type
            });
            await _unitOfWork.CommitAsync();
        }
    }
}
