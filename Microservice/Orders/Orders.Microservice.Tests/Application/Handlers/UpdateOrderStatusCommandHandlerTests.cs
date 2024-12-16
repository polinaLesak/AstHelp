using Moq;
using Orders.Microservice.Application.Commands;
using Orders.Microservice.Application.Exceptions;
using Orders.Microservice.Application.Handlers;
using Orders.Microservice.Domain.Entities;
using Orders.Microservice.Domain.Repositories;
using Orders.Microservice.Infrastructure.Messaging;

namespace Orders.Microservice.Tests.Application.Handlers;

public class UpdateOrderStatusCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenOrderNotFound()
    {
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var rabbitMQProducerMock = new Mock<RabbitMQProducer>();

        unitOfWorkMock.Setup(u => u.Orders.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Order)null);

        var handler = new UpdateOrderStatusCommandHandler(unitOfWorkMock.Object, rabbitMQProducerMock.Object);
        var command = new UpdateOrderStatusCommand { OrderId = Guid.NewGuid(), NewStatus = OrderStatus.Packaged };

        await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldUpdateStatusAndPublishEvent_WhenOrderExists()
    {
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var rabbitMQProducerMock = new Mock<RabbitMQProducer>();
        var id = Guid.NewGuid();
        var order = new Order { Id = id, CustomerId = 123, CreatedAt = DateTime.UtcNow };

        unitOfWorkMock.Setup(u => u.Orders.GetByIdAsync(id))
            .ReturnsAsync(order);
        unitOfWorkMock.Setup(u => u.Orders.Update(It.IsAny<Order>()));
        unitOfWorkMock.Setup(x => x.CommitAsync()).ReturnsAsync(1);

        var handler = new UpdateOrderStatusCommandHandler(unitOfWorkMock.Object, rabbitMQProducerMock.Object);
        var command = new UpdateOrderStatusCommand { OrderId = id, NewStatus = OrderStatus.Packaged };

        await handler.Handle(command, CancellationToken.None);

        Assert.Equal(OrderStatus.Packaged, order.Status);
        unitOfWorkMock.Verify(u => u.Orders.Update(order), Times.Once);
        rabbitMQProducerMock.Verify(p => p.Publish(It.IsAny<object>()), Times.Once);
    }
}