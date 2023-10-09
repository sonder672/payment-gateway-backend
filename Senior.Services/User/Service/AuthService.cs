using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senior.Services.Helper;
using Senior.Services.User.Contracts;
using Senior.Services.User.DTOs.IN;

namespace Senior.Services.User.Service
{
    public class AuthService : IAuthService
    {
        private readonly IEncryption _hash;
        private readonly IJsonWebToken _jsonWebToken;
        private readonly IUserRepository _userDB;

        public AuthService(IEncryption hash, IJsonWebToken jsonWebToken, IUserRepository userDB)
        {
            _hash = hash;
            _jsonWebToken = jsonWebToken;
            _userDB = userDB;
        }

        public async Task<GeneralServiceResponse> Login(string email, string password)
        {
            var user = await this._userDB.GetByEmail(email);

            if (user is null)
            {
                return GeneralServiceResponse.Error(
                    "Wrong email or password",
                    StatusCodeEnum.NotFound);
            }

            var correctPassword = this._hash.ComparePassword(
                password,
                user.Password
            );

            string jsonWebToken = this._jsonWebToken.Generate(user.userUuid);

            if (!correctPassword)
            {
                return GeneralServiceResponse.Error(
                    "Wrong email or password",
                    StatusCodeEnum.BadRequest);
            }

            return GeneralServiceResponse.Success(
                    StatusCodeEnum.OK,
                    new { jwt = jsonWebToken, canSell = user.CanSell });
        }

        public async Task<GeneralServiceResponse> Register(Register userData)
        {
            var user = await this._userDB.GetByEmail(userData.Email);

            if (user is not null)
            {
                return GeneralServiceResponse.Error(
                    "Email in use, use another",
                    StatusCodeEnum.BadRequest);
            }

            if (userData.Password.Length < 9)
            {
                return GeneralServiceResponse.Error(
                    "Password must be more than 8 characters",
                    StatusCodeEnum.BadRequest);
            }

            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (!Regex.IsMatch(userData.Email, emailPattern))
            {
                return GeneralServiceResponse.Error(
                    "Sent email is incorrectly formatted",
                    StatusCodeEnum.BadRequest);
            }

            userData.Password = this._hash.Encrypt(userData.Password);

            await this._userDB.Create(userData);

            return GeneralServiceResponse.Success(
                    StatusCodeEnum.OK,
                    new { message = "Successful creation" });
        }
    }
}