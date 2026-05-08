namespace Application.Core;

public interface IAuthService
{
    Task<Result<LoginResponse>> LoginAsync(string identifier, string password);
    Task<Result> RegisterAsync(ApplicationUser user, string password);
}