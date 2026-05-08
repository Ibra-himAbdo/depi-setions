namespace Application.Core;

public interface IAccountService
{
    Task SendConfirmationEmailAsync(ApplicationUser user);
    Task<Result<LoginResponse>> ConfirmEmailAsync(string userId, string token);
    Task<Result> ChangeEmailAsync(string userId, string newEmail);
    Task<Result> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
}
