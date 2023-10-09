using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Senior.Infrastructure.Others.Bcrypt;
using Senior.Infrastructure.Others.Email;
using Senior.Infrastructure.Others.JWT;
using Senior.Services.Purchase.Contracts;
using Senior.Services.User.Contracts;

namespace Senior.Infrastructure.Others
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IEncryption, Encryption>();
            services.AddTransient<IJsonWebToken, JsonWebToken>();
            services.AddTransient<IEmail, EmailService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                }
            );

            return services;
        }
    }
}