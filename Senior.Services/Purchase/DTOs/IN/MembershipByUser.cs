using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Senior.Services.Purchase.DTOs.IN
{
    public class MembershipByUser
    {
        public MembershipByUser(string? userId, string productId)
        {
            UserId = userId;
            ProductId = productId;
        }

        [JsonIgnore]
        public string? UserId { get; set; }
        public string ProductId { get; }
    }
}