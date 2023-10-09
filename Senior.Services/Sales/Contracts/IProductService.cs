using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senior.Services.Helper;
using Senior.Services.Purchase.DTOs.IN;

namespace Senior.Services.Sales.Contracts
{
    public interface IProductService
    {
        public Task<GeneralServiceResponse> Create(DTOs.IN.Product productData);
        public Task<GeneralServiceResponse> GetById(string uuid);
        public Task<GeneralServiceResponse> GetSoldProducts(string userUuid);
        public Task<GeneralServiceResponse> GetTransactions(string userUuid);
        public Task<GeneralServiceResponse> CancelMembership(MembershipByUser membershipData);
        public Task<GeneralServiceResponse> GetPurchasedProducts(string userUuid);
    }
}