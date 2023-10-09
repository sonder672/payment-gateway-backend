using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senior.Services.User.Contracts;

namespace Senior.Infrastructure.Others.Bcrypt
{
    public class Encryption : IEncryption
    {
        public bool ComparePassword(string passwordIN, string passwordDB)
        {
            bool passwordMatch = BCrypt.Net.BCrypt.Verify(passwordIN, passwordDB);

            return passwordMatch;
        }

        public string Encrypt(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            return hashedPassword;
        }
    }
}