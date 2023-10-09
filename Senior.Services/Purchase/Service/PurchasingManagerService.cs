using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Senior.PaymentMicroservice.Contracts;
using Senior.Services.Helper;
using Senior.Services.Purchase.Contracts;
using Senior.Services.Purchase.DTOs.IN;
using Senior.Services.Sales.Contracts;
using Senior.Services.User.Contracts;

namespace Senior.Services.Purchase.Service
{
    public class PurchasingManagerService : IPurchasingManagerService
    {
        private readonly IProductUserRepository _productUserRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMembershipRepository _membershipRepository;
        private readonly ICheckOut _checkOut;
        private readonly IEmail _email;
        private readonly IConfiguration _configuration;

        public PurchasingManagerService(
            IProductUserRepository productUserRepository,
            IProductRepository productRepository,
            IMembershipRepository membershipRepository,
            ICheckOut checkOut,
            IEmail email,
            IConfiguration configuration)
        {
            _productUserRepository = productUserRepository;
            _productRepository = productRepository;
            _membershipRepository = membershipRepository;
            _checkOut = checkOut;
            _email = email;
            _configuration = configuration;
        }

        public async Task<GeneralServiceResponse> BuyProduct(DTOs.IN.PayProduct payData)
        {
            var product = await this._productRepository.GetProductInformation(payData.ProductUuid);

            if (product is null)
            {
                return GeneralServiceResponse.Error(
                    "Product not found",
                    StatusCodeEnum.NotFound);
            }

            var purchaseResult = this._checkOut.Pay(payData.PaymentInformation);

            if (!purchaseResult.IsSuccess)
            {
                await this._productUserRepository.Create(new DTOs.IN.CreateProductUser(
                    false,
                    payData.UserUuid,
                    payData.ProductUuid,
                    payData.Email));

                return GeneralServiceResponse.Error(
                    purchaseResult.Message,
                    StatusCodeEnum.BadRequest);
            }

            await this._productUserRepository.Create(new DTOs.IN.CreateProductUser(
                    true,
                    payData.UserUuid,
                    payData.ProductUuid,
                    payData.Email));

            if (!String.IsNullOrEmpty(payData.UserUuid) && product.Type == ProductType.Membership)
            {
                var membership = await this._membershipRepository.UpdateNextPayment(new MembershipByUser(
                    payData.UserUuid,
                    payData.ProductUuid
                ), (PaymentPeriod)product.Period!);

                if (!membership)
                {
                    await this._membershipRepository.Create(new DTOs.IN.CreateMembership(
                    userId: payData.UserUuid,
                    productId: payData.ProductUuid,
                    true,
                    null), (PaymentPeriod)product.Period!);
                }
            }

            await this._email.SendEmail(new SendEmail(
                payData.Email!,
                EmailHtml.GetPurchaseEmailHtml(
                    product.Name,
                    product.Price.ToString("N2"),
                    $"{this._configuration["Domain"]}/{product.Image}"),
                "Purchase made correctly"
            ));

            return GeneralServiceResponse.Success(
            StatusCodeEnum.OK,
            new { message = "Payment made correctly. Purchased product" });
        }
    }
}