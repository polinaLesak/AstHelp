using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Notification.Microservice.Application.DI;
using Notification.Microservice.Domain.Repositories;
using Notification.Microservice.Infrastructure.Persistence;
using Notification.Microservice.Tests.Configuration;
using System.Diagnostics;

namespace Notification.Microservice.Tests.Application.DI;

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
    public void AddApplicationServices_ShouldRegisterNotificationRepository()
    {
        var notificationRepository = _serviceProvider.GetService<INotificationRepository>();

        Assert.NotNull(notificationRepository);
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