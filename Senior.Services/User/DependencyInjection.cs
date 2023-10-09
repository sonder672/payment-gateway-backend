using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Senior.Services.User.Contracts;
using Senior.Services.User.Service;

namespace Senior.Services.User
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();

            return services;
        }
    }
}