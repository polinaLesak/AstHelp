using Moq;
using Notification.Microservice.Application.Exceptions;
using Notification.Microservice.Application.Handlers;
using Notification.Microservice.Domain.Repositories;
using Orders.Microservice.Application.Commands;
using NotificationEntity = Notification.Microservice.Domain.Entities.Notification;

namespace Notification.Microservice.Tests.Application.Handlers;

public class MarkNotificationAsReadByIdCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly MarkNotificationAsReadByIdCommandHandler _handler;

    public MarkNotificationAsReadByIdCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new MarkNotificationAsReadByIdCommandHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldMarkNotificationAsRead()
    {
        var command = new MarkNotificationAsReadByIdCommand(Guid.NewGuid());
        var notification = new NotificationEntity { Id = command.NotificationId, IsRead = false };

        _unitOfWorkMock.Setup(x => x.Notification.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(notification);

        await _handler.Handle(command, CancellationToken.None);

        Assert.True(notification.IsRead);
        _unitOfWorkMock.Verify(x => x.Notification.Update(It.IsAny<NotificationEntity>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenNotificationNotFound()
    {
        var command = new MarkNotificationAsReadByIdCommand(Guid.NewGuid());

        _unitOfWorkMock.Setup(x => x.Notification.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((NotificationEntity)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }
}