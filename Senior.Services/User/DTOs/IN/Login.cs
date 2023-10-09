using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senior.Services.User.DTOs.IN
{
    public class Login
    {
        public string Email { get; }
        public string Password { get; }

        public Login(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }
    }
}