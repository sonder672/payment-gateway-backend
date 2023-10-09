using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senior.Services.Sales.Contracts
{
    public interface IProductUserRepository
    {
        public Task Create(Purchase.DTOs.IN.CreateProductUser productUserData);
        public Task<DTOs.OUT.SalesInfo> GetSalesInfo(string productUuid);
        public Task<List<DTOs.OUT.Transactions>?> GetAllByUser(string userUuid);
        public Task<List<DTOs.OUT.Transactions>?> GetMembershipsPurchased(string userUuid);
    }
}