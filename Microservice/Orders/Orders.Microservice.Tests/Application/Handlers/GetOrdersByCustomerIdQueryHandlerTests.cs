using Moq;
using Orders.Microservice.Application.Handlers;
using Orders.Microservice.Application.Queries;
using Orders.Microservice.Domain.Entities;
using Orders.Microservice.Domain.Repositories;

namespace Orders.Microservice.Tests.Application.Handlers;

public class GetOrdersByCustomerIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnOrdersForCustomer()
    {
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var orders = new List<Order>
        {
            new Order { Id = Guid.NewGuid(), CustomerFullname = "John Doe" }
        };

        unitOfWorkMock.Setup(u => u.Orders.GetOrdersByCustomerIdAsync(1)).ReturnsAsync(orders);

        var handler = new GetOrdersByCustomerIdQueryHandler(unitOfWorkMock.Object);

        var result = await handler.Handle(new GetOrdersByCustomerIdQuery(1), CancellationToken.None);

        Assert.Equal(orders, result);
        unitOfWorkMock.Verify(u => u.Orders.GetOrdersByCustomerIdAsync(1), Times.Once);
    }
}