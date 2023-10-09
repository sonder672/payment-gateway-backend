using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senior.Services.Purchase.DTOs.IN;

namespace Senior.Services.Sales.Contracts
{
    public interface IProductRepository
    {
        public Task Create(DTOs.IN.Product productData);
        public Task<DTOs.IN.Product?> GetById(string uuid);
        public Task<ProductInformation?> GetProductInformation(string uuid);
        public Task<List<DTOs.IN.Product>?> GetOwn(string userUuid);
    }
}