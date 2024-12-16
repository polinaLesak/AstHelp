using Moq;
using Orders.Microservice.Application.Exceptions;
using Orders.Microservice.Application.Handlers;
using Orders.Microservice.Application.Queries;
using Orders.Microservice.Domain.Entities;
using Orders.Microservice.Domain.Repositories;

namespace Orders.Microservice.Tests.Application.Handlers;

public class GetOrderByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnOrder_WhenOrderExists()
    {
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var id = Guid.NewGuid();
        var order = new Order { Id = id, CustomerFullname = "John Doe" };

        unitOfWorkMock.Setup(u => u.Orders.GetByIdAsync(id)).ReturnsAsync(order);

        var handler = new GetOrderByIdQueryHandler(unitOfWorkMock.Object);

        var result = await handler.Handle(new GetOrderByIdQuery(id), CancellationToken.None);

        Assert.Equal(order, result);
        unitOfWorkMock.Verify(u => u.Orders.GetByIdAsync(id), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenOrderDoesNotExist()
    {
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(u => u.Orders.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Order)null);

        var handler = new GetOrderByIdQueryHandler(unitOfWorkMock.Object);

        await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(new GetOrderByIdQuery(Guid.NewGuid()), CancellationToken.None));
    }
}