using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Orders.Microservice.Application.DI;
using Orders.Microservice.Domain.Repositories;
using Orders.Microservice.Infrastructure.Persistence;
using Orders.Microservice.Tests.Configuration;
using System.Diagnostics;

namespace Orders.Microservice.Tests.Application.DI;

public class DependencyInjectionTests : IClassFixture<PostgreSqlContainerFixture>
{
    private readonly IServiceProvider _serviceProvider;

    public DependencyInjectionTests(PostgreSqlContainerFixture postgreSqlContainerFixture)
    {
        var postgres = postgreSqlContainerFixture;

        IServiceCollection serviceCollection = new ServiceCollection();
        var configurationMock = new Mock<IConfiguration>();
        Debug.Assert(postgres != null, nameof(postgres) + " != null");

        configurationMock.Setup(c => c["ConnectionStrings:DefaultConnection"])
            .Returns(postgres.ConnectionString);

        serviceCollection.AddApplicationServices(configurationMock.Object);
        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    [Fact]
    public void AddApplicationServices_ShouldRegisterDbContext()
    {
        var dbContext = _serviceProvider.GetService<EFDBContext>();

        Assert.NotNull(dbContext);
    }

    [Fact]
    public void AddApplicationServices_ShouldRegisterOrderRepository()
    {
        var orderRepository = _serviceProvider.GetService<IOrderRepository>();

        Assert.NotNull(orderRepository);
    }

    [Fact]
    public void AddApplicationServices_ShouldRegisterUnitOfWork()
    {
        var unitOfWork = _serviceProvider.GetService<IUnitOfWork>();

        Assert.NotNull(unitOfWork);
    }

    [Fact]
    public void AddApplicationServices_ShouldConfigureDbContextWithCorrectConnectionString()
    {
        var dbContext = _serviceProvider.GetService<EFDBContext>();

        Assert.NotNull(dbContext);
    }
}