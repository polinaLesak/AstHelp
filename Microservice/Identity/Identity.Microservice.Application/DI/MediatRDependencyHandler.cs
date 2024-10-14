using Microsoft.Extensions.DependencyInjection;

namespace Identity.Microservice.Application.DI
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
