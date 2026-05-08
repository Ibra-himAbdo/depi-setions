namespace Application.Core;

public interface ITokenService
{
    Task<LoginResponse> CreateJwtTokenAsync(ApplicationUser user);
}
