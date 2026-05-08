using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

internal class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAccountService _accountService;
    private readonly ITokenService _tokenService;

    public AuthService(UserManager<ApplicationUser> userManager, IAccountService accountService, ITokenService tokenService)
    {
        _userManager = userManager;
        _accountService = accountService;
        _tokenService = tokenService;
    }

    public async Task<Result<LoginResponse>> LoginAsync(string identifier, string password)
    {
        var normalizedIdentifier = identifier.ToUpperInvariant();
        ApplicationUser? user = await _userManager.Users.FirstOrDefaultAsync(e => e.NormalizedEmail == normalizedIdentifier || e.NormalizedUserName == normalizedIdentifier);

        if (user is null || !await _userManager.CheckPasswordAsync(user, password))
            return Error.Validation(description: "Invalid Email or Password");

        if (!user.EmailConfirmed)
        {
            await _accountService.SendConfirmationEmailAsync(user);
            return Error.Validation(description: "Please confirm your email");
        }

        return await _tokenService.CreateJwtTokenAsync(user);
    }

    public async Task<Result> RegisterAsync(ApplicationUser user, string password)
    {
        bool isEmailTaken = await _userManager.FindByEmailAsync(user.Email!) is not null;
        if (isEmailTaken) return Error.Conflict(description: "Email already in use");

        bool isUserNameTaken = await _userManager.FindByNameAsync(user.UserName!) is not null;
        if (isUserNameTaken) return Error.Conflict(description: "Username already taken");

        IdentityResult? result;
        result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            result = await _userManager.AddToRoleAsync(user, ApplicationRoles.User);

            if (result.Succeeded)
                return Result.Success();
        }

        IEnumerable<string> errors = result.Errors.Select(e => e.Description);

        return Error.Validation(description: string.Join("\n", errors));
    }

}