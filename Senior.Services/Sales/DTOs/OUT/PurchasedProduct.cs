using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senior.Services.Helper;

namespace Senior.Services.Sales.DTOs.OUT
{
    public class PurchasedProduct
    {
        public PurchasedProduct(string name, string? image, decimal price, ProductType type, PaymentPeriod? paymentPeriod, DateTime purchaseItemDate, DateTime? lastMembershipPaymentDate, DateTime? nextPaymentDate, bool? activeMembership, string productId)
        {
            Name = name;
            Image = image;
            Price = price;
            Type = type;
            PaymentPeriod = paymentPeriod;
            PurchaseItemDate = purchaseItemDate;
            LastMembershipPaymentDate = lastMembershipPaymentDate;
            NextPaymentDate = nextPaymentDate;
            ActiveMembership = activeMembership;
            ProductId = productId;
        }

        public string Name { get; }
        public string? Image { get; }
        public decimal Price { get; }
        public ProductType Type { get; }
        public PaymentPeriod? PaymentPeriod { get; }
        public DateTime PurchaseItemDate { get; }
        public DateTime? LastMembershipPaymentDate { get; }
        public DateTime? NextPaymentDate { get; }
        public bool? ActiveMembership { get; }
        public string ProductId { get; }
    }
}