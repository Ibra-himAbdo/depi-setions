namespace Application.Client;

public class HttpClientController : Controller
{
    private readonly HttpClient _httpClient;

    public HttpClientController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(DependencyInjection.ApplicationEndpoints);
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/login", model);
        if (response.IsSuccessStatusCode)
        {
            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
            if (loginResponse?.Token != null)
            {
                Response.Cookies.Append(ApplicationConstants.CookieName, loginResponse.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = loginResponse.ExpiresOn
                });
            }

            TempData["ToastType"] = "success";
            TempData["ToastMessage"] = "Logged in successfully";
            return RedirectToAction("Index", "Home");
        }

        var errorResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();
        if (errorResponse?.Errors != null)
        {
            foreach (var error in errorResponse.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }

        return View(model);
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/register", model);
        if (response.IsSuccessStatusCode)
        {
            TempData["ToastType"] = "success";
            TempData["ToastMessage"] = "Account created successfully";
            return RedirectToAction(nameof(Login));
        }

        var errorResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();
        if (errorResponse?.Errors != null)
        {
            foreach (var error in errorResponse.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _httpClient.PostAsync("api/auth/logout", null);

        Response.Cookies.Delete(ApplicationConstants.CookieName);

        TempData["ToastType"] = "success";
        TempData["ToastMessage"] = "Logged out successfully";
        return RedirectToAction("Index", "Home");
    }
}