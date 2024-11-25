using Moq;
using Notification.Microservice.Application.EventsHandlers;
using Notification.Microservice.Domain.Repositories;
using Notification.Microservice.Infrastructure.Messaging.Events;

namespace Notification.Microservice.Tests.Application.EventsHandlers;
using NotificationEntity = Notification.Microservice.Domain.Entities.Notification;

public class UserDeletedEventHandlerTests
{
    [Fact]
    public async Task Handle_ShouldMarkNotificationsAsRead_AndRemoveThem()
    {
        var userId = 1;
        var notifications = new List<NotificationEntity>
        {
            new NotificationEntity { Id = Guid.NewGuid(), UserId = userId, IsRead = false },
            new NotificationEntity { Id = Guid.NewGuid(), UserId = userId, IsRead = false }
        };

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockNotificationRepository = new Mock<INotificationRepository>();

        mockNotificationRepository
            .Setup(repo => repo.GetAllNotificationsByUserId(userId))
            .ReturnsAsync(notifications);

        mockUnitOfWork.Setup(uow => uow.Notification).Returns(mockNotificationRepository.Object);

        var handler = new UserDeletedEventHandler(mockUnitOfWork.Object);

        var eventRequest = new UserDeletedEvent { UserId = userId };

        await handler.Handle(eventRequest, CancellationToken.None);

        foreach (var notification in notifications)
        {
            Assert.True(notification.IsRead);
        }

        mockNotificationRepository.Verify(repo => repo.Remove(It.IsAny<NotificationEntity>()), Times.Exactly(notifications.Count));

        mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Once);
    }
}