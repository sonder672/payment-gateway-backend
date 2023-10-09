using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senior.Services.Helper;

namespace Senior.Services.Purchase.DTOs.IN
{
    public class ProductInformation
    {
        public ProductInformation(ProductType type, PaymentPeriod? period, string name, decimal price, string? image)
        {
            Type = type;
            Period = period;
            Name = name;
            Price = price;
            Image = image;
        }

        public PaymentPeriod? Period { get; }
        public Helper.ProductType Type { get; }
        public string Name { get; }
        public decimal Price { get; }
        public string? Image { get; }
    }
}