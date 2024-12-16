using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Moq;
using Orders.Microservice.Application.Commands;
using Orders.Microservice.Application.DTOs;
using Orders.Microservice.Application.Exceptions;
using Orders.Microservice.Application.Handlers;
using Orders.Microservice.Application.Service;
using Orders.Microservice.Domain.Entities;
using Orders.Microservice.Domain.Repositories;

namespace Orders.Microservice.Tests.Application.Handlers;

public class GenerateOrderActCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnWordDocument_WhenOrderExists()
    {
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var fileServiceMock = new Mock<IFileService>();
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        var identityServiceMock = new Mock<IdentityService>();

        Guid id = Guid.NewGuid();
        var order = new Order { Id = id, CustomerFullname = "John Doe" };
        unitOfWorkMock
            .Setup(u => u.Orders.GetByIdAsync(id))
            .ReturnsAsync(order);

        var adminProfile = new Profile { Fullname = "Admin User", Position = "Manager" };
        identityServiceMock
            .Setup(i => i.GetUserInfoById(It.IsAny<int>()))
            .ReturnsAsync(new UserInfo { Profile = adminProfile });

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, "123")
        };

        var httpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity(claims)) };
        httpContextAccessorMock.Setup(h => h.HttpContext).Returns(httpContext);

        fileServiceMock
            .Setup(f => f.GetTemplateStream(It.IsAny<string>()))
            .Returns(new MemoryStream());

        var handler = new GenerateOrderActCommandHandler(
            unitOfWorkMock.Object,
            fileServiceMock.Object,
            httpContextAccessorMock.Object,
            identityServiceMock.Object);

        var result = await handler.Handle(new GenerateOrderActCommand(id), CancellationToken.None);

        Assert.NotNull(result);
        Assert.IsType<byte[]>(result);
        unitOfWorkMock.Verify(u => u.Orders.GetByIdAsync(id), Times.Once);
        identityServiceMock.Verify(i => i.GetUserInfoById(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenOrderDoesNotExist()
    {
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var fileServiceMock = new Mock<IFileService>();
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        var identityServiceMock = new Mock<IdentityService>();

        unitOfWorkMock
            .Setup(u => u.Orders.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Order)null);

        var handler = new GenerateOrderActCommandHandler(
            unitOfWorkMock.Object,
            fileServiceMock.Object,
            httpContextAccessorMock.Object,
            identityServiceMock.Object);

        await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(new GenerateOrderActCommand(Guid.NewGuid()), CancellationToken.None));
    }
}