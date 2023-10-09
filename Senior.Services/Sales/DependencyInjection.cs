using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Senior.Services.Sales.Contracts;
using Senior.Services.Sales.Service;

namespace Senior.Services.Sales
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            //? Resolución de interfaces 'repository' en capa superior (infrastructure)

            return services;
        }
    }
}