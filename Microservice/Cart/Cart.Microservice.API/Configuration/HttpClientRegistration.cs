using Cart.Microservice.Application.Service;

namespace Cart.Microservice.Application.DI
{
    public static class HttpClientRegistration
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<MachineTokenService>(client =>
            {
                client.BaseAddress = new Uri(configuration["IdentityService:BaseUrl"]);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddHttpClient<CatalogService>(client =>
            {
                client.BaseAddress = new Uri(configuration["CatalogService:BaseUrl"]);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            return services;
        }
    }
}
