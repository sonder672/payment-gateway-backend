using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senior.Services.Helper;
using Senior.Services.Purchase.DTOs.IN;

namespace Senior.Services.Purchase.Contracts
{
    public interface IMembershipRepository
    {
        public Task Create(CreateMembership membershipData, PaymentPeriod period);
        public Task<CreateMembership?> GetMembershipByUser(MembershipByUser membership);
        public Task CancelMembership(MembershipByUser membership, bool status);
        public Task<bool> UpdateNextPayment(MembershipByUser membership, PaymentPeriod period);
    }
}