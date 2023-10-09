using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senior.Services.Helper;

namespace Senior.Services.Purchase.Contracts
{
    public interface IEmail
    {
        public Task SendEmail(SendEmail email);
    }
}