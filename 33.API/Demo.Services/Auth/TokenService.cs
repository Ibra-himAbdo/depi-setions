using Demo.Core.Abstractions;
using Demo.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Demo.Services.Auth;

public class TokenService : ITokenService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;

    public TokenService(UserManager<AppUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<LoginResponse> CreateTokenAsync(AppUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        // get the claims
        List<Claim> claims =
        [
            new(ClaimTypes.NameIdentifier,user.Id),
            new(ClaimTypes.Email,user.Email),
            new(ClaimTypes.GivenName,user.FullName)
        ];

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));


        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        DateTime expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Jwt:DurationInMinuties"));


        JwtSecurityToken token = new(claims: claims, expires: expires, signingCredentials: signingCredentials);

        string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

        return new(accessToken, expires);

    }
}