using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senior.Services.User.Contracts
{
    public interface IEncryption
    {
        public string Encrypt(string password);
        public bool ComparePassword(string passwordIN, string passwordDB);
    }
}