using Microsoft.AspNetCore.Localization;
using System.Diagnostics;
using System.Globalization;

namespace Application.Client;

public class HomeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> IndexAsync()
    {
        var productCount = await _unitOfWork.Repository<Product>().CountAsync();
        var brandCount = await _unitOfWork.Repository<ProductBrand>().CountAsync();
        var categoryCount = await _unitOfWork.Repository<ProductCategory>().CountAsync();

        ViewBag.ProductCount = productCount;
        ViewBag.BrandCount = brandCount;
        ViewBag.CategoryCount = categoryCount;

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

        return LocalRedirect(Url.Action(nameof(IndexAsync)) ?? "/");
    }
}