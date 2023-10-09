using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Senior.Services.User.Contracts;

namespace Senior.Infrastructure.Others.JWT
{
    public class JsonWebToken : IJsonWebToken
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;

        public JsonWebToken(
            IConfiguration configuration,
            IHttpContextAccessor contextAccessor
        )
        {
            this._configuration = configuration;
            this._contextAccessor = contextAccessor;
        }

        public string Generate(string userId)
        {
            SymmetricSecurityKey? securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(this._configuration["Jwt:Key"]!)
            );

            SigningCredentials? credentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256
            );

            Claim[]? claims = new[] { new Claim("userId", userId.ToString()) };

            int expirationTimeInMinutes = Int32.Parse(this._configuration["Jwt:expirationTimeInMinutes"]!);

            JwtSecurityToken? token = new JwtSecurityToken(
                this._configuration["Jwt:Issuer"],
                this._configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(expirationTimeInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GetClaimUserId()
        {
            var userId = this
                ._contextAccessor
                .HttpContext?
                .User
                .FindFirst("UserId")?
                .Value;

            if (userId == null)
            {
                throw new Exception("claim UserId is not in JWT token");
            }

            return userId;
        }
    }
}