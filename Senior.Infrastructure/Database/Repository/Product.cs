using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Senior.Services.Purchase.DTOs.IN;
using Senior.Services.Sales.Contracts;

namespace Senior.Infrastructure.Database.Repository
{
    public class Product : IProductRepository
    {
        private readonly Context _context;

        public Product(Context context)
        {
            _context = context;
        }

        public async Task Create(Services.Sales.DTOs.IN.Product productData)
        {
            var newProduct = new Models.Product
            {
                Id = productData.Uuid,
                Name = productData.Name,
                Price = productData.Price,
                Image = productData.Image,
                Stock = productData.Stock,
                Type = productData.Type,
                PaymentPeriod = productData.PaymentPeriod,
                UserId = productData.UserUuid!,
                Url = productData.Url
            };

            this._context.Product.Add(newProduct);

            await this._context.SaveChangesAsync();
        }

        public async Task<Services.Sales.DTOs.IN.Product?> GetById(string uuid)
        {
            return await this._context.Product
                .Where(x => x.Id == uuid)
                .Select(x => new Services.Sales.DTOs.IN.Product(
                    x.Name,
                    x.Image,
                    x.UserId,
                    x.Price,
                    x.Stock,
                    x.Type,
                    x.PaymentPeriod,
                    x.Id,
                    x.Url
                ))
                .FirstOrDefaultAsync();
        }

        public async Task<List<Services.Sales.DTOs.IN.Product>?> GetOwn(string userUuid)
        {
            return await this._context.Product
                .Where(x => x.UserId == userUuid)
                .Select(x => new Services.Sales.DTOs.IN.Product(
                    x.Name,
                    x.Image,
                    x.UserId,
                    x.Price,
                    x.Stock,
                    x.Type,
                    x.PaymentPeriod,
                    x.Id,
                    x.Url
                ))
                .ToListAsync();
        }

        public async Task<ProductInformation?> GetProductInformation(string uuid)
        {
            return await this._context.Product
                .Where(x => x.Id == uuid)
                .Select(x => new ProductInformation(
                    x.Type,
                    x.PaymentPeriod,
                    x.Name,
                    x.Price,
                    x.Image))
                .FirstOrDefaultAsync();
        }
    }
}