using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Senior.Services.Sales.Contracts;
using Senior.Services.User.Contracts;
using Senior.Services.Purchase.DTOs.IN;

namespace Senior.Infrastructure.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IJsonWebToken _jsonWebToken;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SalesController(IProductService productService, IJsonWebToken jsonWebToken, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _jsonWebToken = jsonWebToken;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProduct([FromQuery] string productUuid)
        {
            var response = await this._productService.GetById(productUuid);

            if (response.IsSuccess)
            {
                return StatusCode((int)response.StatusCode, response.SuccessMessage);
            }

            return StatusCode((int)response.StatusCode, new { message = response.ErrorMessage });
        }

        [HttpPost("UploadImage")]
        [Authorize]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
        {
            if (file is null || file.Length < 1)
            {
                return BadRequest(new { message = "No image received" });
            }

            Console.WriteLine(this._webHostEnvironment.ContentRootPath);
            string filePath = Path.Combine(this._webHostEnvironment.ContentRootPath, "public", "images");

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string uniqueName = $"{DateTime.Now.Ticks}_{file.FileName}";
            string imagePath = Path.Combine(filePath, uniqueName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            string imageUrl = $"public/images/{uniqueName}";

            return Ok(new { imageUrl });
        }

        [HttpPost("CreateProduct")]
        [Authorize]
        public async Task<IActionResult> CreateProduct(Senior.Services.Sales.DTOs.IN.Product request)
        {
            var userUuid = this._jsonWebToken.GetClaimUserId();
            request.UserUuid = userUuid;

            var response = await this._productService.Create(request);

            if (response.IsSuccess)
            {
                return StatusCode((int)response.StatusCode, response.SuccessMessage);
            }

            return StatusCode((int)response.StatusCode, new { message = response.ErrorMessage });
        }

        [HttpGet("GetSoldProducts")]
        [Authorize]
        public async Task<IActionResult> GetSoldProducts()
        {
            var userUuid = this._jsonWebToken.GetClaimUserId();

            var response = await this._productService.GetSoldProducts(userUuid);

            if (response.IsSuccess)
            {
                return StatusCode((int)response.StatusCode, response.SuccessMessage);
            }

            return StatusCode((int)response.StatusCode, new { message = response.ErrorMessage });
        }

        [HttpPost("CancelMembership")]
        [Authorize]
        public async Task<IActionResult> CancelMembership(MembershipByUser request)
        {
            var userUuid = this._jsonWebToken.GetClaimUserId();

            request.UserId = userUuid;
            var response = await this._productService.CancelMembership(request);

            if (response.IsSuccess)
            {
                return StatusCode((int)response.StatusCode, response.SuccessMessage);
            }

            return StatusCode((int)response.StatusCode, new { message = response.ErrorMessage });
        }
    }
}