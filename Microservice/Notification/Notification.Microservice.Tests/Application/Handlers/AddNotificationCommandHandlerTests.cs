using Moq;
using Notification.Microservice.Application.Handlers;
using Notification.Microservice.Domain.Repositories;
using Orders.Microservice.Application.Commands;
using NotificationEntity = Notification.Microservice.Domain.Entities.Notification;
using NotificationEntityType = Notification.Microservice.Domain.Entities.NotificationType;

namespace Notification.Microservice.Tests.Application.Handlers;

public class AddNotificationCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly AddNotificationCommandHandler _handler;

    public AddNotificationCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new AddNotificationCommandHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldAddNotificationAndReturnEntity()
    {
        var command = new AddNotificationCommand
        {
            UserId = 1,
            Title = "Test Title",
            Message = "Test Message",
            Type = NotificationEntityType.Success
        };

        _unitOfWorkMock.Setup(x => x.Notification.AddAsync(It.IsAny<NotificationEntity>()));
        _unitOfWorkMock.Setup(x => x.CommitAsync()).ReturnsAsync(1);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(command.UserId, result.UserId);
        Assert.Equal(command.Message, result.Message);
        _unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);
    }
}