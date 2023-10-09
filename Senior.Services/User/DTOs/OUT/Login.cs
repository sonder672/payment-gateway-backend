using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senior.Services.User.DTOs.OUT
{
    public class Login
    {
        public Login(string password, string userUuid, bool? canSell)
        {
            Password = password;
            this.userUuid = userUuid;
            CanSell = canSell;
        }

        public string Password { get; }
        public string userUuid { get; }
        public bool? CanSell { get; }
    }
}