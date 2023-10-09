using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Senior.Infrastructure.Database.Repository;
using Senior.Services.Purchase.Contracts;
using Senior.Services.Sales.Contracts;
using Senior.Services.User.Contracts;

namespace Senior.Infrastructure.Database
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDbContext(
            this IServiceCollection services,
            IConfiguration configuration
            )
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));
            services.AddDbContext<Context>(options =>
                options.UseMySql(
                    configuration.GetConnectionString("ConnectionDB"),
                    serverVersion
                ).UseSnakeCaseNamingConvention(),
                ServiceLifetime.Transient
            );

            services.AddTransient<IUserRepository, User>();
            services.AddTransient<IProductRepository, Product>();
            services.AddTransient<IProductUserRepository, ProductUser>();
            services.AddTransient<IMembershipRepository, Membership>();

            return services;
        }
    }
}