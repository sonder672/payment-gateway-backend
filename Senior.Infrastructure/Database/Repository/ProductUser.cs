using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Senior.Services.Helper;
using Senior.Services.Sales.Contracts;
using Senior.Services.Sales.DTOs.OUT;

namespace Senior.Infrastructure.Database.Repository
{
    public class ProductUser : IProductUserRepository
    {
        private readonly Context _context;

        public ProductUser(Context context)
        {
            _context = context;
        }

        public async Task Create(Senior.Services.Purchase.DTOs.IN.CreateProductUser productUserData)
        {
            var newProductUser = new Models.ProductUser
            {
                UserId = productUserData.UserUuid,
                ProductId = productUserData.ProductUuid,
                Status = productUserData.PurchaseStatus,
                Date = productUserData.Date,
                Email = productUserData.Email,
                Id = productUserData.Id
            };

            this._context.ProductUser.Add(newProductUser);

            await this._context.SaveChangesAsync();
        }

        public async Task<List<Transactions>?> GetAllByUser(string userUuid)
        {
            var transactions = await _context.ProductUser
                .Where(pu => pu.UserId == userUuid)
                .Select(pu => new Transactions(
                    pu.Product.Name,
                    pu.Product.Id,
                    pu.Product.Type,
                    pu.Product.Price,
                    pu.Status,
                    pu.Date,
                    pu.Product.Image,
                    pu.Product.PaymentPeriod))
                .ToListAsync();

            var salesMade = await _context.Product
                .Where(pu => pu.UserId == userUuid)
                .Select(pu => pu.Id)
                .ToListAsync();

            var sales = await _context.ProductUser
                .Where(pu => salesMade.Contains(pu.ProductId) && pu.Status)
                .Select(pu => new Transactions(
                    pu.Product.Name,
                    pu.Product.Id,
                    pu.Product.Type,
                    pu.Product.Price,
                    pu.Status,
                    pu.Date,
                    pu.Product.Image,
                    pu.Product.PaymentPeriod)
                {
                    IsOwner = true
                })
                .ToListAsync();

            return transactions.Concat(sales).ToList();
        }

        public async Task<List<Transactions>?> GetMembershipsPurchased(string userUuid)
        {
            return await this._context.ProductUser
                .Where(pu => pu.UserId == userUuid
                    && pu.Product.Type == ProductType.Membership)
                .Select(pu => new Transactions(
                    pu.Product.Name,
                    pu.Product.Id,
                    pu.Product.Type,
                    pu.Product.Price,
                    pu.Status,
                    pu.Date,
                    pu.Product.Image,
                    pu.Product.PaymentPeriod))
                .Distinct()
                .ToListAsync();
        }

        public async Task<SalesInfo> GetSalesInfo(string productUuid)
        {
            var salesInfo = await _context.ProductUser
                .Where(um => um.ProductId == productUuid && um.Status == true)
                .GroupBy(um => um.ProductId)
                .Select(group => new SalesInfo
                {
                    TotalSold = group.Sum(um => um.Product.Price),
                    QuantitySold = group.Count()
                })
                .FirstOrDefaultAsync();

            if (salesInfo is null)
            {
                return new SalesInfo
                {
                    TotalSold = 0,
                    QuantitySold = 0
                };
            }

            return salesInfo;
        }
    }
}