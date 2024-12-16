using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Moq;
using Orders.Microservice.Application.Commands;
using Orders.Microservice.Application.Handlers;
using Orders.Microservice.Domain.Entities;
using Orders.Microservice.Domain.Repositories;

namespace Orders.Microservice.Tests.Application.Handlers;

public class GenerateAllOrdersReportCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnExcel_WhenUserIsAdmin()
    {
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

        var orders = new List<Order>
        {
            new Order { Id = Guid.NewGuid(), CustomerFullname = "John Doe" },
            new Order { Id = Guid.NewGuid(), CustomerFullname = "Jane Doe" }
        };

        unitOfWorkMock
            .Setup(u => u.Orders.GetAllAsync())
            .ReturnsAsync(orders);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Role, "1"),
            new Claim(ClaimTypes.NameIdentifier, "123")
        };

        var httpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity(claims)) };
        httpContextAccessorMock.Setup(h => h.HttpContext).Returns(httpContext);

        var handler = new GenerateAllOrdersReportCommandHandler(unitOfWorkMock.Object, httpContextAccessorMock.Object);

        var result = await handler.Handle(new GenerateAllOrdersReportCommand(), CancellationToken.None);

        Assert.NotNull(result);
        Assert.IsType<byte[]>(result);
        unitOfWorkMock.Verify(u => u.Orders.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyArray_WhenUserIsNotAuthenticated()
    {
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

        httpContextAccessorMock.Setup(h => h.HttpContext).Returns((HttpContext)null);

        var handler = new GenerateAllOrdersReportCommandHandler(unitOfWorkMock.Object, httpContextAccessorMock.Object);

        var result = await handler.Handle(new GenerateAllOrdersReportCommand(), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(result);
    }
}