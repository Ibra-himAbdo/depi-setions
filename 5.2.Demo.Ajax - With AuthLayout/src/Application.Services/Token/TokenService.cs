namespace Application.Services;

internal class TokenService : ITokenService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtSettings _jwtSettings;

    public TokenService(UserManager<ApplicationUser> userManager, IOptions<JwtSettings> options)
    {
        _userManager = userManager;
        _jwtSettings = options.Value;
    }

    public async Task<LoginResponse> CreateJwtTokenAsync(ApplicationUser user)
    {
        IList<string> userRoles = await _userManager.GetRolesAsync(user);

        List<Claim> claims = new()
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.GivenName, user.FullName!),
        };

        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        SigningCredentials signingCredentials = new(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(_jwtSettings.DurationInDays),
            signingCredentials: signingCredentials);

        return new LoginResponse(new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo, user.EmailConfirmed);
    }
}
