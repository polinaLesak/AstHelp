using Notification.Microservice.Domain.Repositories;
using Notification.Microservice.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Notification.Microservice.Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EFDBContext>(options =>
                options.UseNpgsql(configuration["ConnectionStrings:DefaultConnection"]));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
