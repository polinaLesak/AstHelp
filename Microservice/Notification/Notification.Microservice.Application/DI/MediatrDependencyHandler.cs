﻿using Microsoft.Extensions.DependencyInjection;

namespace Notification.Microservice.Application.DI
{
    public static class MediatrDependencyHandler
    {
        public static IServiceCollection RegisterMediatrHandlers(this IServiceCollection services)
        {
            return services.AddMediatR(cf =>
                cf.RegisterServicesFromAssembly(typeof(MediatrDependencyHandler).Assembly
            ));
        }
    }
}
