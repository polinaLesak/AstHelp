using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notification.Microservice.Domain.Repositories;
using Notification.Microservice.Infrastructure.Persistence;
using Notification.Microservice.Infrastructure.Repositories;

namespace Notification.Microservice.Application.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<EFDBContext>(options =>
            options.UseNpgsql(configuration["ConnectionStrings:DefaultConnection"]));

        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}