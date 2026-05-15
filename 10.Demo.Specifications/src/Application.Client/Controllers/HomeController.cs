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

    public IActionResult ChangeLanguage(string? selectedLanguageCode)
    {
        if (string.IsNullOrWhiteSpace(selectedLanguageCode))
            selectedLanguageCode = "en-US";

        Response.Cookies.Append(
            ApplicationConstants.LanguageCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(selectedLanguageCode)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
        );

        Uri? referer = Request.GetTypedHeaders().Referer;
        if (referer is not null)
        {
            string localPath = referer.PathAndQuery + referer.Fragment;
            if (Url.IsLocalUrl(localPath))
                return LocalRedirect(localPath);
        }

        return RedirectToAction(nameof(Index));
    }
}