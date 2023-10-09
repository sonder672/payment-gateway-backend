using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Senior.Services.Helper;
using Senior.Services.Sales.Contracts;
using Senior.Services.User.Contracts;
using Senior.Services.Purchase.Contracts;
using Senior.Services.Purchase.DTOs.IN;

namespace Senior.Services.Sales.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductUserRepository _productUserRepository;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public ProductService(
            IProductRepository productRepository,
            IProductUserRepository productUserRepository,
            IMembershipRepository membershipRepository,
            IUserRepository userRepository,
            IConfiguration configuration)
        {
            _productRepository = productRepository;
            _productUserRepository = productUserRepository;
            _membershipRepository = membershipRepository;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<GeneralServiceResponse> Create(DTOs.IN.Product productData)
        {
            var userExists = await this._userRepository.UserCanSell(productData.UserUuid!);

            if (!userExists)
            {
                return GeneralServiceResponse.Error(
                    "Internal error",
                    StatusCodeEnum.NotFound);
            }

            await this._productRepository.Create(productData);
            return GeneralServiceResponse.Success(
                StatusCodeEnum.Created,

                new { Url = productData.Url });
        }

        public async Task<GeneralServiceResponse> GetById(string uuid)
        {
            var product = await this._productRepository.GetById(uuid);

            if (product is null)
            {
                return GeneralServiceResponse.Error(
                    "Product not found",
                    StatusCodeEnum.NotFound);
            }

            return GeneralServiceResponse.Success(
                StatusCodeEnum.OK,
                product);
        }

        public async Task<GeneralServiceResponse> GetSoldProducts(string userUuid)
        {
            var products = await this._productRepository.GetOwn(userUuid);

            if (products is null)
            {
                return GeneralServiceResponse.Error(
                   "This user has no products",
                   StatusCodeEnum.NotFound);
            }

            var productUuids = products
                .Select(product => product.Uuid)
                .ToList();

            var salesInfoDictionary = new Dictionary<string, DTOs.OUT.SalesInfo>();
            foreach (var uuid in productUuids)
            {
                var salesInfo = await this._productUserRepository.GetSalesInfo(uuid);
                salesInfoDictionary[uuid] = salesInfo;
            }

            var productsCompleteInformation = products.Select(product =>
            {
                var salesInfo = salesInfoDictionary[product.Uuid];

                return new DTOs.OUT.ProductSales(
                    name: product.Name,
                    image: product.Image,
                    price: product.Price,
                    stock: product.Stock,
                    uuid: product.Uuid,
                    url: product.Url,
                    amountSold: salesInfo.TotalSold,
                    totalSold: salesInfo.QuantitySold
                );
            }).ToList();

            return GeneralServiceResponse.Success(
            StatusCodeEnum.OK,
            new { productList = productsCompleteInformation });
        }

        public async Task<GeneralServiceResponse> GetTransactions(string userUuid)
        {
            var transactions = await this._productUserRepository.GetAllByUser(userUuid);

            if (transactions is null)
            {
                return GeneralServiceResponse.Error(
                   "This user has no transactions",
                   StatusCodeEnum.NotFound);
            }

            return GeneralServiceResponse.Success(
            StatusCodeEnum.OK,
            new { transactions = transactions });
        }

        public async Task<GeneralServiceResponse> GetPurchasedProducts(string userUuid)
        {
            var products = await this._productUserRepository.GetMembershipsPurchased(userUuid);

            if (products is null)
            {
                return GeneralServiceResponse.Error(
                   "This user has no memberships",
                   StatusCodeEnum.NotFound);
            }

            List<DTOs.OUT.PurchasedProduct> membershipProducts = new List<DTOs.OUT.PurchasedProduct>();
            var uniqueUserIdProductIdPairs = new HashSet<(string UserId, string ProductId)>();

            foreach (var product in products)
            {
                var userIdProductIdPair = (userUuid, product.ProductUuid);

                if (uniqueUserIdProductIdPairs.Contains(userIdProductIdPair))
                {
                    continue;
                }

                var membership = await this._membershipRepository.GetMembershipByUser(new MembershipByUser(
                    userUuid,
                    product.ProductUuid));

                if (membership is null)
                {
                    continue;
                }

                var purchasedProduct = new DTOs.OUT.PurchasedProduct(
                    name: product.ProductName,
                    image: product.Image,
                    price: product.ProductPrice,
                    type: product.ProductType,
                    paymentPeriod: product.PaymentPeriod,
                    purchaseItemDate: product.Date,
                    lastMembershipPaymentDate: membership.Date,
                    nextPaymentDate: membership.NextPaymentDate,
                    activeMembership: membership.Status,
                    productId: product.ProductUuid
                );

                membershipProducts.Add(purchasedProduct);

                uniqueUserIdProductIdPairs.Add(userIdProductIdPair);
            }

            return GeneralServiceResponse.Success(
            StatusCodeEnum.OK,
            new { products = membershipProducts });
        }

        public async Task<GeneralServiceResponse> CancelMembership(MembershipByUser membershipData)
        {
            var membership = await this._membershipRepository.GetMembershipByUser(membershipData);

            if (membership is null)
            {
                return GeneralServiceResponse.Error(
                   "This user doesnt have that membership",
                   StatusCodeEnum.NotFound);
            }

            if (!membership.Status)
            {
                return GeneralServiceResponse.Error(
                   "Cannot cancel a membership that is ALREADY cancel",
                   StatusCodeEnum.BadRequest);
            }

            await this._membershipRepository.CancelMembership(membershipData, false);
            var daysToPay = DateTime.Now - membership.Date;
            var differenceDays = daysToPay.Days;

            return GeneralServiceResponse.Success(
           StatusCodeEnum.OK,
           new { message = $"Your membership has been successfully cancelled. You will soon be notified for the {differenceDays} days pay " });
        }
    }
}