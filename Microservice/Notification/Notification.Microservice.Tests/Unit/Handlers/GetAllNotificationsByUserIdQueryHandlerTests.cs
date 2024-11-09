using Moq;
using Notification.Microservice.Application.Handlers;
using Notification.Microservice.Domain.Repositories;
using Notifications.Microservice.Application.Queries;
using Orders.Microservice.Application.Commands;
using NotificationEntity = Notification.Microservice.Domain.Entities.Notification;

namespace Notification.Microservice.Tests.Unit.Handlers
{
    public class GetAllNotificationsByUserIdQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly GetAllNotificationsByUserIdQueryHandler _handler;

        public GetAllNotificationsByUserIdQueryHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new GetAllNotificationsByUserIdQueryHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnAllNotifications()
        {
            var query = new GetAllNotificationsByUserIdQuery();
            var notifications = new List<NotificationEntity>
            {
                new NotificationEntity { UserId = 1, Title = "Test Title1", Message = "Test Message1" },
                new NotificationEntity { UserId = 1, Title = "Test Title2", Message = "Test Message2" }
            };

            _unitOfWorkMock.Setup(x => x.Notification.GetAllAsync()).ReturnsAsync(notifications);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}
