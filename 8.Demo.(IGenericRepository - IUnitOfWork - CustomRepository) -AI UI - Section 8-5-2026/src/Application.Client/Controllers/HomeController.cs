using Microsoft.AspNetCore.Localization;
using System.Diagnostics;
using System.Globalization;

namespace Application.Client;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet]
    public IActionResult ChangeLanguage(string? selectedLanguageCode, string? returnUrl = null)
    {
        if (string.IsNullOrWhiteSpace(selectedLanguageCode))
            selectedLanguageCode = "en-US";

        Response.Cookies.Append(
            ApplicationConstants.LanguageCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(selectedLanguageCode)),
            new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1),
                Path = "/",
                IsEssential = true,
                SameSite = SameSiteMode.Lax,
            }
        );

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return LocalRedirect(returnUrl);

        var referer = Request.GetTypedHeaders().Referer?.ToString();
        if (!string.IsNullOrEmpty(referer)
            && Uri.TryCreate(referer, UriKind.Absolute, out var refererUri)
            && string.Equals(refererUri.Host, Request.Host.Host, StringComparison.OrdinalIgnoreCase))
            return LocalRedirect(refererUri.PathAndQuery);

        return LocalRedirect(Url.Action(nameof(Index)) ?? "/");
    }
}