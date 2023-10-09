using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Senior.Services.Purchase.Contracts;
using Senior.Services.Purchase.Service;

namespace Senior.Services.Purchase
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            Senior.PaymentMicroservice.DependencyInjection.AddServices(services);
            services.AddTransient<IPurchasingManagerService, PurchasingManagerService>();

            return services;
        }
    }
}