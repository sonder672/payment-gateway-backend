using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senior.Services.User.Contracts
{
    public interface IUserRepository
    {
        public Task<DTOs.OUT.Login?> GetByEmail(string email);
        public Task<bool> UserCanSell(string uuid);
        public Task Create(DTOs.IN.Register userData);
    }
}