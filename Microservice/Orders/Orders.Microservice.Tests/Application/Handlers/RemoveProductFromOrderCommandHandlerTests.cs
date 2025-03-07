using Moq;
using Orders.Microservice.Application.Commands;
using Orders.Microservice.Application.Events;
using Orders.Microservice.Application.Exceptions;
using Orders.Microservice.Application.Handlers;
using Orders.Microservice.Domain.Entities;
using Orders.Microservice.Domain.Repositories;
using Orders.Microservice.Infrastructure.Messaging;

namespace Orders.Microservice.Tests.Application.Handlers;

public class RemoveProductFromOrderCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldRemoveProductAndPublishNotification()
    {
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var rabbitMQProducerMock = new Mock<RabbitMQProducer>();
        var orderId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        var order = new Order
        {
            Id = orderId,
            CustomerId = 123,
            Items = new List<OrderItem>
            {
                new OrderItem { ProductId = productId, ProductName = "Product 1" }
            }
        };

        unitOfWorkMock.Setup(u => u.Orders.GetByIdAsync(orderId)).ReturnsAsync(order);
        unitOfWorkMock.Setup(x => x.CommitAsync()).ReturnsAsync(1);

        var handler = new RemoveProductFromOrderCommandHandler(unitOfWorkMock.Object, rabbitMQProducerMock.Object);

        await handler.Handle(new RemoveProductFromOrderCommand { OrderId = orderId, ProductId = productId }, CancellationToken.None);

        Assert.Empty(order.Items);
        unitOfWorkMock.Verify(u => u.Orders.Update(order), Times.Once);
        unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        rabbitMQProducerMock.Verify(r => r.Publish(It.IsAny<AddNotificationEvent>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenOrderDoesNotExist()
    {
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var rabbitMQProducerMock = new Mock<RabbitMQProducer>();

        unitOfWorkMock.Setup(u => u.Orders.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Order)null);

        var handler = new RemoveProductFromOrderCommandHandler(unitOfWorkMock.Object, rabbitMQProducerMock.Object);

        await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(new RemoveProductFromOrderCommand { OrderId = Guid.NewGuid(), ProductId = Guid.NewGuid() }, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenProductDoesNotExistInOrder()
    {
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var rabbitMQProducerMock = new Mock<RabbitMQProducer>();
        var id = Guid.NewGuid();
        var order = new Order
        {
            Id = id,
            Items = new List<OrderItem>()
        };

        unitOfWorkMock.Setup(u => u.Orders.GetByIdAsync(id)).ReturnsAsync(order);

        var handler = new RemoveProductFromOrderCommandHandler(unitOfWorkMock.Object, rabbitMQProducerMock.Object);

        await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(new RemoveProductFromOrderCommand { OrderId = id, ProductId = Guid.NewGuid() }, CancellationToken.None));
    }
}