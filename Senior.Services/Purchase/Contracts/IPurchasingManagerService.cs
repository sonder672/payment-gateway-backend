using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senior.Services.Helper;

namespace Senior.Services.Purchase.Contracts
{
    public interface IPurchasingManagerService
    {
        public Task<GeneralServiceResponse> BuyProduct(DTOs.IN.PayProduct payData);
    }
}