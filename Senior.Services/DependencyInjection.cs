using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Senior.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            Sales.DependencyInjection.AddServices(services);
            User.DependencyInjection.AddServices(services);
            Purchase.DependencyInjection.AddServices(services);

            return services;
        }
    }
}