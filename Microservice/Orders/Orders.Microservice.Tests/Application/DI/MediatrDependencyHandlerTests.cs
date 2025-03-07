using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Orders.Microservice.Application.DI;

namespace Orders.Microservice.Tests.Application.DI;

public class MediatrDependencyHandlerTests
{
    [Fact]
    public void RegisterMediatrHandlers_ShouldRegisterMediatrServices()
    {
        var services = new ServiceCollection();

        services.RegisterMediatrHandlers();

        var provider = services.BuildServiceProvider();
        var mediator = provider.GetService<IMediator>();
        Assert.NotNull(mediator);
    }
}