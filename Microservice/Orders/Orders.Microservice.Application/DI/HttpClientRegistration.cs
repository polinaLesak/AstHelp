using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orders.Microservice.Application.Service;

namespace Orders.Microservice.Application.DI
{
    public static class HttpClientRegistration
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            // HttpClient для MachineTokenService
            services.AddHttpClient<MachineTokenService>(client =>
            {
                client.BaseAddress = new Uri(configuration["IdentityService:BaseUrl"]);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            // HttpClient для Identity сервиса
            services.AddHttpClient<IdentityService>(client =>
            {
                client.BaseAddress = new Uri(configuration["IdentityService:BaseUrl"]);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            return services;
        }
    }
}
