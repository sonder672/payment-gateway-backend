using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Senior.Services.Helper;
using Senior.Services.Purchase.Contracts;
using Senior.Services.Purchase.DTOs.IN;

namespace Senior.Infrastructure.Database.Repository
{
    public class Membership : IMembershipRepository
    {
        private readonly Context _context;

        public Membership(Context context)
        {
            _context = context;
        }

        public async Task CancelMembership(MembershipByUser membership, bool status)
        {
            var membershipData = await this._context.Membership
                .FirstOrDefaultAsync(x => x.ProductId == membership.ProductId && x.UserId == membership.UserId);

            if (membershipData is not null)
            {
                membershipData.Status = status;

                this._context.Membership.Update(membershipData);
                await this._context.SaveChangesAsync();
            }
        }

        public async Task<bool> UpdateNextPayment(MembershipByUser membership, PaymentPeriod period)
        {
            var membershipData = await this._context.Membership
               .FirstOrDefaultAsync(x => x.ProductId == membership.ProductId && x.UserId == membership.UserId);

            if (membershipData is null)
            {
                return false;
            }

            DateTime nextPayment = membershipData.NextPaymentDate is null
                ? DateTime.Now
                : (DateTime)membershipData.NextPaymentDate;

            if (period == PaymentPeriod.Daily)
            {
                nextPayment = nextPayment.AddDays(1);
            }

            if (period == PaymentPeriod.HalfMonth)
            {
                nextPayment = nextPayment.AddDays(15);
            }

            if (period == PaymentPeriod.OneMonth)
            {
                nextPayment = nextPayment.AddMonths(1);
            }

            membershipData.NextPaymentDate = nextPayment;

            this._context.Membership.Update(membershipData);
            await this._context.SaveChangesAsync();

            return true;
        }

        public async Task Create(CreateMembership membershipData, PaymentPeriod period)
        {
            var newMembership = new Models.Membership
            {
                Id = membershipData.Id,
                ProductId = membershipData.ProductId,
                UserId = membershipData.UserId,
                Status = membershipData.Status,
                DatePaymentMade = membershipData.Date
            };

            DateTime nextPayment = newMembership.DatePaymentMade;
            if (period == PaymentPeriod.Daily)
            {
                nextPayment = nextPayment.AddDays(1);
            }

            if (period == PaymentPeriod.HalfMonth)
            {
                nextPayment = nextPayment.AddDays(15);
            }

            if (period == PaymentPeriod.OneMonth)
            {
                nextPayment = nextPayment.AddMonths(1);
            }

            newMembership.NextPaymentDate = nextPayment;

            this._context.Membership.Add(newMembership);

            await this._context.SaveChangesAsync();
        }

        public async Task<CreateMembership?> GetMembershipByUser(MembershipByUser membership)
        {
            return await this._context.Membership
                .Where(x => x.UserId == membership.UserId
                    && x.ProductId == membership.ProductId
                    && x.Product.Type == ProductType.Membership)
                .Select(x => new CreateMembership(
                    x.UserId,
                    x.ProductId,
                    x.Status,
                    x.NextPaymentDate,
                    x.Id,
                    x.DatePaymentMade
                )).FirstOrDefaultAsync();
        }
    }
}