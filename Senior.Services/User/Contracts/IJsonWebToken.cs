using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senior.Services.User.Contracts
{
    public interface IJsonWebToken
    {
        public string Generate(string userId);
        public string GetClaimUserId();
    }
}