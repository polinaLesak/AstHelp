using Microsoft.Extensions.DependencyInjection;

namespace Notification.Microservice.Application.DI
{
    public static class MediatRDependencyHandler
    {
        public static IServiceCollection RegisterRequestHandlers(this IServiceCollection services)
        {
            return services
                .AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(MediatRDependencyHandler).Assembly));
        }
    }
}
