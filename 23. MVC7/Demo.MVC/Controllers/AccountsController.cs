using Demo.MVC.Models;
using Microsoft.AspNetCore.Identity;

namespace Demo.MVC.Controllers;

public class AccountsController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountsController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Login(string? returnUrl)
    {
        var model = new LoginViewModel()
        {
            RetrunUrl = returnUrl
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            AppUser? user = await _userManager.FindByEmailAsync(model.Email!);

            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Email Or Password");
                return View(model);
            }

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(
                user, model.Password!, isPersistent: model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                if (!string.IsNullOrWhiteSpace(model.RetrunUrl) && Url.IsLocalUrl(model.RetrunUrl))
                {
                    return Redirect(model.RetrunUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid Email Or Password");
        }

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}
