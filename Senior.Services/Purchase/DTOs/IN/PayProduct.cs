using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Senior.PaymentMicroservice.DTOs.IN;

namespace Senior.Services.Purchase.DTOs.IN
{
    public class PayProduct
    {
        public PayProduct(Pay paymentInformation, string productUuid, string? email, string? name)
        {
            PaymentInformation = paymentInformation;
            ProductUuid = productUuid;
            Email = email;
            Name = name;
        }

        public Senior.PaymentMicroservice.DTOs.IN.Pay PaymentInformation { get; }
        public string ProductUuid { get; }

        [JsonIgnore]
        public string? UserUuid { get; set; }
        public string? Email { get; }
        public string? Name { get; }
    }
}