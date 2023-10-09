using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Senior.Services.Purchase.Contracts;
using Senior.Services.Sales.Contracts;
using Senior.Services.User.Contracts;

namespace Senior.Infrastructure.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchasingManagerService _purchaseService;
        private readonly IProductService _productService;
        private readonly IJsonWebToken _jsonWebToken;

        public PurchaseController(IPurchasingManagerService purchaseService, IProductService productService, IJsonWebToken jsonWebToken)
        {
            _purchaseService = purchaseService;
            _productService = productService;
            _jsonWebToken = jsonWebToken;
        }

        [HttpPost("PurchaseProduct")]
        public async Task<IActionResult> PurchaseProduct(Senior.Services.Purchase.DTOs.IN.PayProduct request)
        {
            try
            {
                var userUuid = this._jsonWebToken.GetClaimUserId();
                request.UserUuid = userUuid;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            var response = await this._purchaseService.BuyProduct(request);

            if (response.IsSuccess)
            {
                return StatusCode((int)response.StatusCode, response.SuccessMessage);
            }

            return StatusCode((int)response.StatusCode, new { message = response.ErrorMessage });
        }

        [HttpGet("GetTransactions")]
        [Authorize]
        public async Task<IActionResult> GetTransactions()
        {
            var userUuid = this._jsonWebToken.GetClaimUserId();

            var response = await this._productService.GetTransactions(userUuid);

            if (response.IsSuccess)
            {
                return StatusCode((int)response.StatusCode, response.SuccessMessage);
            }

            return StatusCode((int)response.StatusCode, new { message = response.ErrorMessage });
        }

        [HttpGet("GetPurchasedProducts")]
        [Authorize]
        public async Task<IActionResult> GetPurchasedProducts()
        {
            var userUuid = this._jsonWebToken.GetClaimUserId();

            var response = await this._productService.GetPurchasedProducts(userUuid);

            if (response.IsSuccess)
            {
                return StatusCode((int)response.StatusCode, response.SuccessMessage);
            }

            return StatusCode((int)response.StatusCode, new { message = response.ErrorMessage });
        }
    }
}