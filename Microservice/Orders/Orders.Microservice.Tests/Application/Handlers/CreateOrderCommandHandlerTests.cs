using Moq;
using Orders.Microservice.Application.Commands;
using Orders.Microservice.Application.DTOs;
using Orders.Microservice.Application.Handlers;
using Orders.Microservice.Application.Service;
using Orders.Microservice.Domain.Entities;
using Orders.Microservice.Domain.Repositories;

namespace Orders.Microservice.Tests.Application.Handlers;

public class CreateOrderCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCreateOrderSuccessfully()
    {
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var identityServiceMock = new Mock<IdentityService>();
        var catalogServiceMock = new Mock<CatalogService>();
        var cartServiceMock = new Mock<CartService>();

        var command = new CreateOrderCommand
        {
            CustomerId = 1,
            ReasonForIssue = "Test",
            Items = new List<OrderItemDto>
            {
                new OrderItemDto { ProductId = Guid.NewGuid(), Quantity = 2 }
            }
        };

        var managers = new List<UserInfo>
        {
            new UserInfo { Id = 2, Profile = new Profile { Fullname = "Manager 1", Position = "Manager" } }
        };

        var customerInfo = new UserInfo
        {
            Id = 1,
            Profile = new Profile { Fullname = "Customer 1", Position = "Customer" }
        };

        var productsInfo = new List<ProductInfoDto>
        {
            new ProductInfoDto { ProductId = Guid.NewGuid(), CatalogId = 1, CatalogName = "Catalog 1", ProductName = "Product 1" }
        };

        identityServiceMock.Setup(s => s.GetAllManagersAsync())
            .ReturnsAsync(managers);
        identityServiceMock.Setup(s => s.GetUserInfoById(1))
            .ReturnsAsync(customerInfo);
        catalogServiceMock.Setup(s => s.GetProductsInfoAsync(It.IsAny<Guid[]>()))
            .ReturnsAsync(productsInfo);
        unitOfWorkMock.Setup(u => u.Orders.AddAsync(It.IsAny<Order>())).Returns(Task.CompletedTask);
        unitOfWorkMock.Setup(x => x.CommitAsync()).ReturnsAsync(1);

        var handler = new CreateOrderCommandHandler(unitOfWorkMock.Object, identityServiceMock.Object, catalogServiceMock.Object, cartServiceMock.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(command.CustomerId, result.CustomerId);
        Assert.Equal(command.ReasonForIssue, result.ReasonForIssue);
        Assert.Single(result.Items);
        Assert.Equal("Product 1", result.Items.First().ProductName);
    }
}