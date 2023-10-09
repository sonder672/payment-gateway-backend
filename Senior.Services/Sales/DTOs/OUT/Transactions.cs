using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senior.Services.Helper;

namespace Senior.Services.Sales.DTOs.OUT
{
    public class Transactions
    {
        public Transactions(string productName, string productUuid, ProductType productType, decimal productPrice, bool status, DateTime date, string? image = null, PaymentPeriod? paymentPeriod = null)
        {
            ProductName = productName;
            ProductUuid = productUuid;
            ProductType = productType;
            ProductPrice = productPrice;
            Status = status;
            Date = date;
            Image = image;
            PaymentPeriod = paymentPeriod;
        }

        public string ProductName { get; }
        public string ProductUuid { get; }
        public Helper.ProductType ProductType { get; }
        public decimal ProductPrice { get; }
        public bool Status { get; }
        public DateTime Date { get; }
        public string? Image { get; }
        public Helper.PaymentPeriod? PaymentPeriod { get; }
        public bool? IsOwner { get; set; }
    }
}