using Microsoft.Extensions.DependencyInjection;

namespace Cart.Microservice.Application.DI
{
    public static class CORS
    {
        public static IServiceCollection AddCORS(this IServiceCollection services, string specificOrigins)
        {
            return services.AddCors(options =>
            {
                options.AddPolicy(name: specificOrigins,
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
        }
    }
}
