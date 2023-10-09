using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senior.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            Senior.Services.DependencyInjection.AddServices(services);
            Others.DependencyInjection.AddServices(services, configuration);
            services.AddTransient<Middleware.ErrorHandlerMiddleware>();

            //? DbContext EFCore
            Senior.Infrastructure.Database.DependencyInjection.AddDbContext(services, configuration);

            return services;
        }
    }
}