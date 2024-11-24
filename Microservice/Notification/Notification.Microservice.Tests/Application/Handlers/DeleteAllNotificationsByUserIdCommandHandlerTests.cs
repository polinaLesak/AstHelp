using Moq;
using Notification.Microservice.Application.Handlers;
using Notification.Microservice.Domain.Repositories;
using Orders.Microservice.Application.Commands;
using NotificationEntity = Notification.Microservice.Domain.Entities.Notification;

namespace Notification.Microservice.Tests.Application.Handlers;

public class DeleteAllNotificationsByUserIdCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly DeleteAllNotificationsByUserIdCommandHandler _handler;

    public DeleteAllNotificationsByUserIdCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new DeleteAllNotificationsByUserIdCommandHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldDeleteAllNotificationsByUserId()
    {
        var command = new DeleteAllNotificationsByUserIdCommand(1);
        var notifications = new List<NotificationEntity> { new() { Id = Guid.NewGuid(), UserId = 1 } };

        _unitOfWorkMock.Setup(x => x.Notification.GetAllNotificationsByUserId(It.IsAny<int>()))
            .ReturnsAsync(notifications);

        await _handler.Handle(command, CancellationToken.None);

        _unitOfWorkMock.Verify(x => x.Notification.Remove(It.IsAny<NotificationEntity>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);
    }
}