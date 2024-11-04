namespace Catalog.Microservice.API.Configuration
{
    public static class MediatRDependencyHandler
    {
        public static IServiceCollection RegisterMediatrHandlers(this IServiceCollection services)
        {
            return services
                .AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(MediatRDependencyHandler).Assembly));
        }
    }
}
