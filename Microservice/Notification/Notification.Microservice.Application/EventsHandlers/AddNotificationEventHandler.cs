using MediatR;
using Notification.Microservice.Domain.Repositories;
using Notification.Microservice.Infrastructure.Messaging.Events;
using NotificationEntity = Notification.Microservice.Domain.Entities.Notification;

namespace Notification.Microservice.Application.EventsHandlers;

public class AddNotificationEventHandler(IUnitOfWork unitOfWork) : INotificationHandler<AddNotificationEvent>
{
    public async Task Handle(AddNotificationEvent notification, CancellationToken cancellationToken)
    {
        await unitOfWork.Notification.AddAsync(new NotificationEntity
        {
            UserId = notification.UserId,
            Title = notification.Title,
            Message = notification.Message,
            Timestamp = DateTime.UtcNow,
            Type = notification.Type
        });
        await unitOfWork.CommitAsync();
    }
}