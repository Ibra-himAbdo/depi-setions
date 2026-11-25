using Dome.Core.Abstractions;
using Dome.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Dome.Services.Auth;

public class TokenService : ITokenService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;

    public TokenService(UserManager<AppUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }
    public async Task<string> CreateTokenAsync(AppUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        List<Claim> claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier,user.Id),
            new(ClaimTypes.Email,user.Email),
            new(ClaimTypes.GivenName,user.FullName),
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["JWT:SecritKey"]));

        SigningCredentials signingCredentials = 
            new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);


        DateTime expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("JWT:DurationInMininsts"));


        JwtSecurityToken token = new(claims: claims, expires: expires, signingCredentials: signingCredentials);

        string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

        return accessToken;
    }
}
