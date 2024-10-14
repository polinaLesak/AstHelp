using Identity.Microservice.Application.Services;
using Identity.Microservice.Domain.Repositories;
using Identity.Microservice.Infrastructure.Persistence;
using Identity.Microservice.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Microservice.Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EFDBContext>(options =>
                options.UseNpgsql(configuration["ConnectionStrings:DefaultConnection"]));

            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
