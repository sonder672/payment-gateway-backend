using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senior.Services.Helper;

namespace Senior.Services.Purchase.DTOs.IN
{
    public class CreateProductUser
    {
        public CreateProductUser(bool purchaseStatus, string? userUuid, string productUuid, string? email)
        {
            PurchaseStatus = purchaseStatus;
            UserUuid = userUuid;
            ProductUuid = productUuid;
            Date = DateTime.Now;
            Id = UUID.Generate();

            if (string.IsNullOrEmpty(userUuid) && string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("Email cannot be null");
            }

            Email = string.IsNullOrEmpty(userUuid) ? email : null;
        }

        public bool PurchaseStatus { get; }
        public string Id { get; }
        public string? UserUuid { get; }
        public string ProductUuid { get; }
        public string? Email { get; }
        public DateTime Date { get; }
    }
}