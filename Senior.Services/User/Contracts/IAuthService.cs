using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senior.Services.Helper;

namespace Senior.Services.User.Contracts
{
    public interface IAuthService
    {
        public Task<GeneralServiceResponse> Login(string email, string password);
        public Task<GeneralServiceResponse> Register(DTOs.IN.Register userData);
    }
}