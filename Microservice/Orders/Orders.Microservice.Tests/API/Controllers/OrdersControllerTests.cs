using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Orders.Microservice.API.Controllers;
using Orders.Microservice.Application.Commands;
using Orders.Microservice.Application.DTOs;
using Orders.Microservice.Application.Queries;
using Orders.Microservice.Domain.Entities;

namespace Orders.Microservice.Tests.API.Controllers;

public class OrdersControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly OrdersController _controller;

    public OrdersControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new OrdersController(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetAllOrders_ShouldReturnOrders()
    {
        var orders = new List<Order>
        {
            new Order { Id = Guid.NewGuid(), CustomerId = 1, ManagerId = 2 },
            new Order { Id = Guid.NewGuid(), CustomerId = 3, ManagerId = 4 }
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllOrdersQuery>(), default))
            .ReturnsAsync(orders);

        var result = await _controller.GetAllOrders();

        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Order>>(result);
        Assert.Equal(orders.Count, result.Count());
        _mediatorMock.Verify(m => m.Send(It.IsAny<GetAllOrdersQuery>(), default), Times.Once);
    }

    [Fact]
    public async Task GetOrderById_ShouldReturnOrder()
    {
        var orderId = Guid.NewGuid();
        var order = new Order { Id = orderId, CustomerId = 1, ManagerId = 2 };

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetOrderByIdQuery>(), default))
            .ReturnsAsync(order);

        var result = await _controller.GetOrderById(orderId);

        Assert.NotNull(result);
        Assert.Equal(orderId, result.Id);
        _mediatorMock.Verify(m => m.Send(It.IsAny<GetOrderByIdQuery>(), default), Times.Once);
    }

    [Fact]
    public async Task GetOrdersByUserId_ShouldReturnOrders()
    {
        var customerId = 1;
        var orders = new List<Order>
        {
            new Order { Id = Guid.NewGuid(), CustomerId = customerId, ManagerId = 2 }
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetOrdersByCustomerIdQuery>(), default))
            .ReturnsAsync(orders);

        var result = await _controller.GetOrdersByUserId(customerId);

        Assert.NotNull(result);
        Assert.Equal(orders.Count, result.Count());
        _mediatorMock.Verify(m => m.Send(It.IsAny<GetOrdersByCustomerIdQuery>(), default), Times.Once);
    }

    [Fact]
    public async Task CreateOrder_ShouldReturnCreatedOrder()
    {
        var command = new CreateOrderCommand
        {
            CustomerId = 1,
            Items = [new OrderItemDto { ProductId = Guid.NewGuid(), Quantity = 2 }]
        };
        var order = new Order { Id = Guid.NewGuid(), CustomerId = command.CustomerId };

        _mediatorMock.Setup(m => m.Send(command, default))
            .ReturnsAsync(order);

        var result = await _controller.CreateOrder(command);

        Assert.NotNull(result);
        Assert.Equal(order.Id, result.Id);
        _mediatorMock.Verify(m => m.Send(command, default), Times.Once);
    }

    [Fact]
    public async Task UpdateOrderStatus_ShouldCallMediator()
    {
        var orderId = Guid.NewGuid();
        var status = OrderStatus.Performed;

        await _controller.UpdateOrderStatus(orderId, status);

        _mediatorMock.Verify(m => m.Send(It.IsAny<UpdateOrderStatusCommand>(), default), Times.Once);
    }

    [Fact]
    public async Task GenerateOrderAct_ShouldReturnFileResult()
    {
        var orderId = Guid.NewGuid();
        var fileBytes = new byte[] { 0x1, 0x2, 0x3 };

        _mediatorMock.Setup(m => m.Send(It.IsAny<GenerateOrderActCommand>(), default))
            .ReturnsAsync(fileBytes);

        var result = await _controller.GenerateOrderAct(orderId);

        Assert.NotNull(result);
        Assert.IsType<FileContentResult>(result);
        Assert.Equal(fileBytes, ((FileContentResult)result).FileContents);
        _mediatorMock.Verify(m => m.Send(It.IsAny<GenerateOrderActCommand>(), default), Times.Once);
    }

    [Fact]
    public async Task GenerateReport_ShouldReturnFileResult()
    {
        var orderId = Guid.NewGuid();
        var fileBytes = new byte[] { 0x1, 0x2, 0x3 };

        _mediatorMock.Setup(m => m.Send(It.IsAny<GenerateOrderReportCommand>(), default))
            .ReturnsAsync(fileBytes);

        var result = await _controller.GenerateReport(orderId);

        Assert.NotNull(result);
        Assert.IsType<FileContentResult>(result);
        Assert.Equal(fileBytes, ((FileContentResult)result).FileContents);
        _mediatorMock.Verify(m => m.Send(It.IsAny<GenerateOrderReportCommand>(), default), Times.Once);
    }

    [Fact]
    public async Task GenerateAllReport_ShouldReturnFileResult()
    {
        var fileBytes = new byte[] { 0x1, 0x2, 0x3 };

        _mediatorMock.Setup(m => m.Send(It.IsAny<GenerateAllOrdersReportCommand>(), default))
            .ReturnsAsync(fileBytes);

        var result = await _controller.GenerateAllReport();

        Assert.NotNull(result);
        Assert.IsType<FileContentResult>(result);
        Assert.Equal(fileBytes, ((FileContentResult)result).FileContents);
        _mediatorMock.Verify(m => m.Send(It.IsAny<GenerateAllOrdersReportCommand>(), default), Times.Once);
    }
    
    [Fact]
    public async Task GetOrdersByManagerId_ShouldReturnOrders()
    {
        var managerId = 2;
        var orders = new List<Order>
        {
            new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = 1,
                ManagerId = managerId,
                Status = OrderStatus.Pending
            },
            new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = 3,
                ManagerId = managerId,
                Status = OrderStatus.Processing
            }
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetOrdersByManagerIdQuery>(), default))
            .ReturnsAsync(orders);

        var result = await _controller.GetOrdersByManagerId(managerId);

        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Order>>(result);
        Assert.Equal(orders.Count, result.Count());
        Assert.All(result, order => Assert.Equal(managerId, order.ManagerId));
        _mediatorMock.Verify(m => m.Send(It.Is<GetOrdersByManagerIdQuery>(q => q.ManagerId == managerId), default), Times.Once);
    }

    [Fact]
    public async Task RemoveProductFromOrder_ShouldCallMediator()
    {
        var orderId = Guid.NewGuid();
        var productId = Guid.NewGuid();

        _mediatorMock.Setup(m => m.Send(It.IsAny<RemoveProductFromOrderCommand>(), default))
            .Returns(Task.CompletedTask);

        await _controller.UpdateOrderStatus(orderId, productId);

        _mediatorMock.Verify(m => m.Send(It.Is<RemoveProductFromOrderCommand>(cmd =>
            cmd.OrderId == orderId && cmd.ProductId == productId), default), Times.Once);
    }

}