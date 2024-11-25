using MediatR;
using Notification.Microservice.Domain.Repositories;
using Notification.Microservice.Infrastructure.Messaging.Events;
using NotificationEntity = Notification.Microservice.Domain.Entities.Notification;

namespace Notification.Microservice.Application.EventsHandlers;

public class UserDeletedEventHandler(IUnitOfWork unitOfWork) : INotificationHandler<UserDeletedEvent>
{
    public async Task Handle(UserDeletedEvent request, CancellationToken cancellationToken)
    {
        List<NotificationEntity> notifications =
            await unitOfWork.Notification.GetAllNotificationsByUserId(request.UserId);
        notifications.ForEach(x =>
        {
            x.IsRead = true;
            unitOfWork.Notification.Remove(x);
        });
        await unitOfWork.CommitAsync();
    }
}