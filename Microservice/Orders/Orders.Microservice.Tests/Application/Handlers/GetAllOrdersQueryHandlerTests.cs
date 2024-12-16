using Moq;
using Orders.Microservice.Application.Handlers;
using Orders.Microservice.Application.Queries;
using Orders.Microservice.Domain.Entities;
using Orders.Microservice.Domain.Repositories;

namespace Orders.Microservice.Tests.Application.Handlers;

public class GetAllOrdersQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnAllOrders()
    {
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var orders = new List<Order>
        {
            new Order { Id = Guid.NewGuid(), CustomerFullname = "John Doe" },
            new Order { Id = Guid.NewGuid(), CustomerFullname = "Jane Doe" }
        };

        unitOfWorkMock.Setup(u => u.Orders.GetAllAsync()).ReturnsAsync(orders);

        var handler = new GetAllOrdersQueryHandler(unitOfWorkMock.Object);

        var result = await handler.Handle(new GetAllOrdersQuery(), CancellationToken.None);

        Assert.Equal(orders, result);
        unitOfWorkMock.Verify(u => u.Orders.GetAllAsync(), Times.Once);
    }
}