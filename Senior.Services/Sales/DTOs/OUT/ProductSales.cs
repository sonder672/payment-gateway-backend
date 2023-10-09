using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senior.Services.Sales.DTOs.OUT
{
    public class ProductSales
    {
        public ProductSales(string name, string? image, decimal price, int? stock, string uuid, string url, decimal amountSold, int totalSold)
        {
            Name = name;
            Image = image;
            Price = price;
            Stock = stock;
            Uuid = uuid;
            Url = url;
            AmountSold = amountSold;
            QuantitySold = totalSold;
        }

        public string Name { get; }
        public string? Image { get; }
        public decimal Price { get; }
        public int? Stock { get; set; }
        public string Uuid { get; }
        public string Url { get; }
        public decimal AmountSold { get; }
        public int QuantitySold { get; }
    }
}