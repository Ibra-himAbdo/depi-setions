using Hangfire;
using Microsoft.AspNetCore.WebUtilities;

namespace Application.Services;

internal class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ITokenService _tokenService;
    private readonly IEmailTemplateService _templateService;
    private readonly IBackgroundJobClient _jobClient;

    public AccountService(UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        ITokenService tokenService,
        IEmailTemplateService templateService,
        IBackgroundJobClient jobClient)
    {
        _userManager = userManager;
        _configuration = configuration;
        _tokenService = tokenService;
        _templateService = templateService;
        _jobClient = jobClient;
    }

    public async Task SendConfirmationEmailAsync(ApplicationUser user)
    {
        string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        string actionLink = GenerateResetLink(_configuration["BaseAppUri"]!, user.Id, token);

        string body = _templateService.GenerateEmailBody(
            title: "Verify Your Email",
            userName: user.FullName!,
            content: "Welcome to our application! Please confirm your email address to get started and unlock all features.",
            actionLink: actionLink,
            actionText: "Confirm Email"
        );

        Email emailModel = new(
            To: [user.Email!],
            Title: "Verify Your Email",
            Subject: "Email Verification",
            Body: body);

        _jobClient.Enqueue<IEmailService>(e => e.SendEmailAsync(emailModel));
    }

    public async Task<Result<LoginResponse>> ConfirmEmailAsync(string userId, string token)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(userId);

        if (user is null) return Error.Validation(description: "Email Not Confirmed");

        string decodedToken;
        try
        {
            decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
        }
        catch (FormatException)
        {
            return Error.Validation(description: "Invalid token");
        }

        var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
        if (!result.Succeeded)
            return Error.Validation(description: string.Join(", ", result.Errors.Select(e => e.Description)));

        return await _tokenService.CreateJwtTokenAsync(user);
    }

    public async Task<Result> ChangeEmailAsync(string userId, string newEmail)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(userId);
        if (user is null) return Error.NotFound(description: "User not found");

        bool isEmailTaken = await _userManager.FindByEmailAsync(newEmail) is not null;
        if (isEmailTaken) return Error.Conflict(description: "Email already in use");

        var result = await _userManager.SetEmailAsync(user, newEmail);
        if (!result.Succeeded)
            return Error.Validation(description: string.Join(", ", result.Errors.Select(e => e.Description)));

        // Update UserName if it was the same as the old email
        if (user.UserName == user.Email)
        {
            await _userManager.SetUserNameAsync(user, newEmail);
        }

        // Send notification to both old and new email? For now just new email
        string notificationBody = _templateService.GenerateEmailBody(
            title: "Email Changed",
            userName: user.FullName!,
            content: $"Your account email has been successfully updated to {newEmail}. If you did not perform this action, please contact support immediately."
        );

        Email emailModel = new(
            To: [newEmail],
            Title: "Email Changed",
            Subject: "Security Notification",
            Body: notificationBody);

        _jobClient.Enqueue<IEmailService>(e => e.SendEmailAsync(emailModel));

        return Result.Success();
    }

    public async Task<Result> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
    {
        if (currentPassword == newPassword)
            return Error.Validation(description: "New password cannot be the same as current password");

        ApplicationUser? user = await _userManager.FindByIdAsync(userId);
        if (user is null) return Error.NotFound(description: "User not found");

        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        if (!result.Succeeded)
            return Error.Validation(description: string.Join(", ", result.Errors.Select(e => e.Description)));

        return Result.Success();
    }

    private static string GenerateResetLink(string url, string userId, string token)
    {
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        return $"{url}/api/Account/ConfirmEmail?userId={Uri.EscapeDataString(userId)}&token={encodedToken}";
    }
}