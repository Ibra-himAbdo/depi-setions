using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Application;

public class AccountController : BaseApiController
{
    private readonly IAccountService _accountService;

    public AccountController(IServiceProvider serviceProvider, IAccountService accountService) : base(serviceProvider)
    {
        _accountService = accountService;
    }

    [HttpGet("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmailAsync([FromQuery] ConfirmEmailDto dto)
    {
        var validationResult = await ValidateRequestModel(dto);
        if (validationResult is not null) return validationResult;

        Result<LoginResponse> serviceResult = await _accountService.ConfirmEmailAsync(dto.UserId, dto.Token);

        return ReturnResult(serviceResult);
    }

    [Authorize]
    [HttpPost("ChangeEmail")]
    public async Task<IActionResult> ChangeEmailAsync(ChangeEmailDto dto)
    {
        var validationResult = await ValidateRequestModel(dto);
        if (validationResult is not null) return validationResult;

        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        Result result = await _accountService.ChangeEmailAsync(userId, dto.NewEmail);

        return ReturnResult(result);
    }

    [Authorize]
    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangePasswordAsync(ChangePasswordDto dto)
    {
        var validationResult = await ValidateRequestModel(dto);
        if (validationResult is not null) return validationResult;

        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        Result result = await _accountService.ChangePasswordAsync(userId, dto.CurrentPassword, dto.NewPassword);

        return ReturnResult(result);
    }
}