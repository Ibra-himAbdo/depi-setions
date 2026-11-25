using Demo.API.Errors;
using Dome.Core.Abstractions;
using Dome.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public AuthController(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            AppUser? user = await _userManager.FindByEmailAsync(email);

            //if (user is null || !await _userManager.CheckPasswordAsync(user, password))
            //    return Unauthorized(new ApiResponse(StatusCodes.Status401Unauthorized, "invalid email or password"));

            if (user is null)
                return Unauthorized(new ApiResponse(StatusCodes.Status401Unauthorized, "invalid email or password"));

            bool isValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if(!isValidPassword)
                return Unauthorized(new ApiResponse(StatusCodes.Status401Unauthorized, "invalid email or password"));

            // generate token 
            var token = await _tokenService.CreateTokenAsync(user);

            return Ok(token);
        }

    }
}
