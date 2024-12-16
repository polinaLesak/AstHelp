using Moq;
using Orders.Microservice.Application.Commands;
using Orders.Microservice.Application.Exceptions;
using Orders.Microservice.Application.Handlers;
using Orders.Microservice.Domain.Entities;
using Orders.Microservice.Domain.Repositories;
using Guid = System.Guid;

namespace Orders.Microservice.Tests.Application.Handlers;

public class GenerateOrderReportCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenOrderNotFound()
    {
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(u => u.Orders.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Order)null);

        var handler = new GenerateOrderReportCommandHandler(unitOfWorkMock.Object);
        var command = new GenerateOrderReportCommand(Guid.NewGuid());

        await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldReturnExcelBytes_WhenOrderExists()
    {
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var id = Guid.NewGuid();
        var order = new Order { Id = id, Items = new List<OrderItem>() };

        unitOfWorkMock.Setup(u => u.Orders.GetByIdAsync(id))
            .ReturnsAsync(order);

        var handler = new GenerateOrderReportCommandHandler(unitOfWorkMock.Object);
        var command = new GenerateOrderReportCommand(id);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.IsType<byte[]>(result);
    }
}