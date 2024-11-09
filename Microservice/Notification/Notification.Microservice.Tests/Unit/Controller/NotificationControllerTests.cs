using MediatR;
using Moq;
using Notification.Microservice.API.Controllers;
using Notification.Microservice.Domain.Repositories;
using Notifications.Microservice.Application.Queries;
using Orders.Microservice.Application.Commands;
using NotificationEntity = Notification.Microservice.Domain.Entities.Notification;

namespace Notification.Microservice.Tests.Unit.Controller
{
    public class NotificationControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly NotificationController _controller;
        private readonly Mock<INotificationRepository> _notificationRepositoryMock;

        public NotificationControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new NotificationController(_mediatorMock.Object);
            _notificationRepositoryMock = new Mock<INotificationRepository>();
        }

        [Fact]
        public async Task GetAllNotificationsByUserId_ShouldReturnNotifications()
        {
            var userId = 1;
            var notifications = new List<NotificationEntity>
            {
                new NotificationEntity { Id = Guid.NewGuid(), Title = "Test Notification", UserId = userId, Message = "This is a test notification" }
            };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllNotificationsByUserIdQuery>(), default))
                .ReturnsAsync(notifications);

            var result = await _controller.GetAllNotificationsByUserId(userId);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<NotificationEntity>>(result);
            Assert.Equal(notifications.Count, result.Count());
            Assert.Equal(notifications[0].Title, result.First().Title);
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetAllNotificationsByUserIdQuery>(), default), Times.Once);
        }

        [Fact]
        public async Task AddNotificationToUserId_ShouldReturnNotification()
        {
            var command = new AddNotificationCommand { UserId = 1, Title = "New Notification", Message = "This is a new notification" };
            var notification = new NotificationEntity
            {
                Id = Guid.NewGuid(),
                Title = command.Title,
                Message = command.Message,
                UserId = command.UserId
            };

            _mediatorMock.Setup(m => m.Send(command, default))
                .ReturnsAsync(notification);

            var result = await _controller.AddNotificationToUserId(command);

            Assert.NotNull(result);
            Assert.Equal(notification.Id, result.Id);
            Assert.Equal(notification.Title, result.Title);
            Assert.Equal(notification.Message, result.Message);
            Assert.Equal(notification.UserId, result.UserId);
            _mediatorMock.Verify(m => m.Send(command, default), Times.Once);
        }

        [Fact]
        public async Task MarkNotificationAsReadById_ShouldCallMediator()
        {
            var notificationId = Guid.NewGuid();
            var notification = new NotificationEntity
            {
                Id = notificationId,
                Title = "Test Notification",
                Message = "This is a test notification",
                UserId = 1,
                IsRead = false
            };

            _notificationRepositoryMock.Setup(repo => repo.GetByIdAsync(notificationId))
                .ReturnsAsync(notification);

            _mediatorMock.Setup(m => m.Send(It.IsAny<MarkNotificationAsReadByIdCommand>(), default))
                .Returns(Task.CompletedTask);

            await _controller.MarkNotificationAsReadById(notificationId);

            _mediatorMock.Verify(m => m.Send(It.IsAny<MarkNotificationAsReadByIdCommand>(), default), Times.Once);
        }

        [Fact]
        public async Task MarkAllNotificationsAsReadByUserId_ShouldCallMediator()
        {
            var userId = 1;
            _mediatorMock.Setup(m => m.Send(It.IsAny<MarkAllNotificationsAsReadByUserIdCommand>(), default))
                .Returns(Task.CompletedTask);

            await _controller.MarkAllNotificationsAsReadByUserId(userId);

            _mediatorMock.Verify(m => m.Send(It.IsAny<MarkAllNotificationsAsReadByUserIdCommand>(), default), Times.Once);
        }

        [Fact]
        public async Task MarkAllNotificationsAsReadByUserId_ShouldCallMediator_And_UpdateNotificationStatuses()
        {
            var userId = 1;
            var notifications = new List<NotificationEntity>
            {
                new NotificationEntity
                {
                    Id = Guid.NewGuid(),
                    Title = "Test Notification 1",
                    Message = "This is a test notification 1",
                    UserId = userId,
                    IsRead = false
                },
                new NotificationEntity
                {
                    Id = Guid.NewGuid(),
                    Title = "Test Notification 2",
                    Message = "This is a test notification 2",
                    UserId = userId,
                    IsRead = false
                }
            };

            _notificationRepositoryMock.Setup(repo => repo.GetAllNotificationsByUserId(userId))
                .ReturnsAsync(notifications);
            _mediatorMock.Setup(m => m.Send(It.IsAny<MarkAllNotificationsAsReadByUserIdCommand>(), default))
                .Returns(Task.CompletedTask);

            await _controller.MarkAllNotificationsAsReadByUserId(userId);

            _mediatorMock.Verify(m => m.Send(It.IsAny<MarkAllNotificationsAsReadByUserIdCommand>(), default), Times.Once);
        }

        [Fact]
        public async Task DeleteNotificationById_ShouldCallMediator()
        {
            var notificationId = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteNotificationByIdCommand>(), default))
                .Returns(Task.CompletedTask);

            await _controller.DeleteNotificationById(notificationId);

            _mediatorMock.Verify(m => m.Send(It.IsAny<DeleteNotificationByIdCommand>(), default), Times.Once);
        }

        [Fact]
        public async Task DeleteAllNotificationsByUserId_ShouldCallMediator()
        {
            var userId = 1;
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteAllNotificationsByUserIdCommand>(), default))
                .Returns(Task.CompletedTask);

            await _controller.DeleteAllNotificationsByUserId(userId);

            _mediatorMock.Verify(m => m.Send(It.IsAny<DeleteAllNotificationsByUserIdCommand>(), default), Times.Once);
        }
    }
}
