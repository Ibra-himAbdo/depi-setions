namespace Application;

public class AuthController : BaseApiController
{
    private readonly IAuthService _authService;

    public AuthController(IServiceProvider serviceProvider, IAuthService authService) : base(serviceProvider)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginDto dto)
    {
        var validationResult = await ValidateRequestModel(dto);
        if (validationResult is not null) return validationResult;

        Result<LoginResponse> serviceResult = await _authService.LoginAsync(dto.Identifier, dto.Password);

        if (serviceResult.IsSuccess)
        {
            Response.Cookies.Append(ApplicationConstants.AuthCookieName, serviceResult.Value?.Token!, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = serviceResult.Value?.ExpiresOn
            });
        }

        return ReturnResult(serviceResult);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync(RegisterDto dto)
    {
        var validationResult = await ValidateRequestModel(dto);
        if (validationResult is not null) return validationResult;

        ApplicationUser user = dto.Adapt<ApplicationUser>();

        Result result = await _authService.RegisterAsync(user, dto.Password);

        return ReturnResult(result, ResponseStatus.Created);
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete(ApplicationConstants.AuthCookieName);
        return Ok();
    }

}