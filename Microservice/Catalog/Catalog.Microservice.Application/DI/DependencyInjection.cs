using Catalog.Microservice.Application.Service;
using Catalog.Microservice.Domain.Repositories;
using Catalog.Microservice.Infrastructure.Persistence;
using Catalog.Microservice.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Microservice.Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            Console.WriteLine("Catalog: " + configuration["ConnectionStrings:DefaultConnection"]);
            services.AddDbContext<EFDBContext>(options =>
                options.UseNpgsql(configuration["ConnectionStrings:DefaultConnection"]));

            services.AddScoped<IAttributeRepository, AttributeRepository>();
            services.AddScoped<IAttributeTypeRepository, AttributeTypeRepository>();
            services.AddScoped<IAttributeValueRepository, AttributeValueRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ICatalogRepository, CatalogRepository>();
            services.AddScoped<ICatalogAttributeRepository, CatalogAttributeRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
