using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Senior.Services.User.Contracts;

namespace Senior.Infrastructure.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _userService;

        public UserController(IAuthService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Services.User.DTOs.IN.Login request)
        {
            var response = await this._userService.Login(request.Email, request.Password);

            if (response.IsSuccess)
            {
                return StatusCode((int)response.StatusCode, response.SuccessMessage);
            }

            return StatusCode((int)response.StatusCode, new { message = response.ErrorMessage });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] Services.User.DTOs.IN.Register request)
        {
            var response = await this._userService.Register(request);

            if (response.IsSuccess)
            {
                return StatusCode((int)response.StatusCode, response.SuccessMessage);
            }

            return StatusCode((int)response.StatusCode, new { message = response.ErrorMessage });
        }
    }
}