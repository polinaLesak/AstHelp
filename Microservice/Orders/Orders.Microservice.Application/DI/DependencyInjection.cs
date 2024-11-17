using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orders.Microservice.Application.Service;
using Orders.Microservice.Domain.Repositories;
using Orders.Microservice.Infrastructure.Persistence;
using Orders.Microservice.Infrastructure.Repositories;

namespace Orders.Microservice.Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EFDBContext>(options =>
                options.UseNpgsql(configuration["ConnectionStrings:DefaultConnection"]));

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFileService, FileService>();

            return services;
        }
    }
}
