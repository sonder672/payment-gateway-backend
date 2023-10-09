using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Senior.PaymentMicroservice.Contracts;
using Senior.PaymentMicroservice.UseCases;

namespace Senior.PaymentMicroservice
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<ICheckOut, CheckOut>();

            return services;
        }
    }
}