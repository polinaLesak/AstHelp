using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Microservice.Application.DI
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
