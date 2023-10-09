using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Senior.Services.Helper;

namespace Senior.Services.Sales.DTOs.IN
{
    public class Product
    {
        public Product(
            string name,
            string? image,
            string? userUuid,
            decimal price,
            int? stock,
            ProductType type,
            PaymentPeriod? paymentPeriod,
            string? uuid = null,
            string? url = null)
        {
            Name = name;
            Image = image;
            UserUuid = userUuid;
            Price = price;
            Stock = stock;
            Type = type;
            PaymentPeriod = paymentPeriod;
            Uuid = String.IsNullOrEmpty(uuid) ? UUID.Generate() : uuid;
            Url = String.IsNullOrEmpty(url) ? $"checkout/{this.Uuid}" : url;
        }

        [JsonIgnore]
        public string? UserUuid { get; set; }
        public string Name { get; }
        public string? Image { get; }
        public decimal Price { get; }
        public int? Stock { get; set; }
        public ProductType Type { get; }
        public PaymentPeriod? PaymentPeriod { get; }

        [System.Text.Json.Serialization.JsonIgnore]
        public string Uuid { get; }
        [System.Text.Json.Serialization.JsonIgnore]
        public string Url { get; }
    }
}