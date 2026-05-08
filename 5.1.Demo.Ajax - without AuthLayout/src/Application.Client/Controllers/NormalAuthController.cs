namespace Application.Client;

public class NormalAuthController : Controller
{
    private readonly IAuthService _authService;
    private readonly IValidator<LoginDto> _loginValidator;
    private readonly IValidator<RegisterDto> _registerValidator;

    public NormalAuthController(IAuthService authService,
                          IValidator<LoginDto> loginValidator,
                          IValidator<RegisterDto> registerValidator)
    {
        _authService = authService;
        _loginValidator = loginValidator;
        _registerValidator = registerValidator;
    }

    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var validationResult = await _loginValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return View(dto);
        }

        var result = await _authService.LoginAsync(dto.Identifier, dto.Password);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Error?.Description ?? "Login failed");
            return View(dto);
        }

        Response.Cookies.Append(ApplicationConstants.CookieName, result.Value?.Token!, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = result.Value?.ExpiresOn
        });

        TempData["ToastType"] = "success";
        TempData["ToastMessage"] = "Logged in successfully";
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var validationResult = await _registerValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return View(dto);
        }

        var user = dto.Adapt<ApplicationUser>();
        var result = await _authService.RegisterAsync(user, dto.Password);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Error?.Description ?? "Registration failed");
            return View(dto);
        }

        TempData["ToastType"] = "success";
        TempData["ToastMessage"] = "Account created successfully";
        return RedirectToAction(nameof(Login));
    }

    [HttpPost]
    public IActionResult Logout()
    {
        Response.Cookies.Delete(ApplicationConstants.CookieName);
        TempData["ToastType"] = "success";
        TempData["ToastMessage"] = "Logged out successfully";
        return RedirectToAction("Index", "Home");
    }
}