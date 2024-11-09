using Moq;
using Notification.Microservice.Application.Handlers;
using Notification.Microservice.Domain.Repositories;
using Orders.Microservice.Application.Commands;
using NotificationEntity = Notification.Microservice.Domain.Entities.Notification;

namespace Notification.Microservice.Tests.Unit.Handlers
{
    public class MarkAllNotificationsAsReadByUserIdCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly MarkAllNotificationsAsReadByUserIdCommandHandler _handler;

        public MarkAllNotificationsAsReadByUserIdCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new MarkAllNotificationsAsReadByUserIdCommandHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldMarkAllNotificationsAsRead()
        {
            var command = new MarkAllNotificationsAsReadByUserIdCommand(1);
            var notifications = new List<NotificationEntity>
            {
                new NotificationEntity { Id = Guid.NewGuid(), UserId = 1, IsRead = false },
                new NotificationEntity { Id = Guid.NewGuid(), UserId = 1, IsRead = false }
            };

            _unitOfWorkMock.Setup(x => x.Notification.GetAllNotificationsByUserId(It.IsAny<int>()))
                           .ReturnsAsync(notifications);

            await _handler.Handle(command, CancellationToken.None);

            Assert.All(notifications, notification => Assert.True(notification.IsRead));
            _unitOfWorkMock.Verify(x => x.Notification.Update(It.IsAny<NotificationEntity>()), Times.Exactly(notifications.Count));
            _unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);
        }
    }
}
