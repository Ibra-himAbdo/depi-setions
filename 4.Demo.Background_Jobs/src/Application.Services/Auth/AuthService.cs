using Hangfire;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

internal class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IBackgroundJobClient _jobClient;

    public AuthService(UserManager<ApplicationUser> userManager, ITokenService tokenService, IBackgroundJobClient jobClient)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _jobClient = jobClient;
    }

    public async Task<Result<LoginResponse>> LoginAsync(string identifier, string password)
    {
        var normalizedIdentifier = identifier.ToUpperInvariant();
        ApplicationUser? user = await _userManager.Users.FirstOrDefaultAsync(e => e.NormalizedEmail == normalizedIdentifier || e.NormalizedUserName == normalizedIdentifier);

        if (user is null || !await _userManager.CheckPasswordAsync(user, password))
            return Error.Validation(description: "Invalid Email or Password");

        if (!user.EmailConfirmed)
        {
            _jobClient.Enqueue<IAccountService>(e => e.SendConfirmationEmailAsync(user));
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