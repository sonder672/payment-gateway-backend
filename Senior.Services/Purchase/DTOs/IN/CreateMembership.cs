using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senior.Services.Helper;

namespace Senior.Services.Purchase.DTOs.IN
{
    public class CreateMembership
    {
        public CreateMembership(string userId, string productId, bool status, DateTime? nextPaymentDate, string? id = null, DateTime? date = null)
        {
            UserId = userId;
            ProductId = productId;
            Status = status;
            NextPaymentDate = nextPaymentDate;
            Id = String.IsNullOrEmpty(id) ? UUID.Generate() : id;
            Date = date is null ? DateTime.Now : (DateTime)date;
        }

        public string Id { get; }
        public string UserId { get; }
        public string ProductId { get; }
        public bool Status { get; }
        public DateTime Date { get; }
        public DateTime? NextPaymentDate { get; }
    }
}