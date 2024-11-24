using Moq;
using Notification.Microservice.Application.Exceptions;
using Notification.Microservice.Application.Handlers;
using Notification.Microservice.Domain.Repositories;
using Orders.Microservice.Application.Commands;
using NotificationEntity = Notification.Microservice.Domain.Entities.Notification;

namespace Notification.Microservice.Tests.Application.Handlers;

public class DeleteNotificationByIdCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly DeleteNotificationByIdCommandHandler _handler;

    public DeleteNotificationByIdCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new DeleteNotificationByIdCommandHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldDeleteNotificationById()
    {
        var command = new DeleteNotificationByIdCommand(Guid.NewGuid());
        var notification = new NotificationEntity { Id = command.NotificationId };

        _unitOfWorkMock.Setup(x => x.Notification.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(notification);

        await _handler.Handle(command, CancellationToken.None);

        _unitOfWorkMock.Verify(x => x.Notification.Remove(It.IsAny<NotificationEntity>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenNotificationNotFound()
    {
        var command = new DeleteNotificationByIdCommand(Guid.NewGuid());

        _unitOfWorkMock.Setup(x => x.Notification.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((NotificationEntity)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }
}