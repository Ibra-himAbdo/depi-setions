using Demo.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Demo.MVC.Controllers
{
    public class AdminsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public AdminsController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var appUser = new AppUser()
            {
                FullName = model.FullName,
                Email = model.Email,
                UserName = model.Email
            };

            IdentityResult result = await _userManager.CreateAsync(appUser, model.Password!);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            AppUser? user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return NotFound();

            UserViewModel model = new()
            {
                Email = user.Email,
                FullName = user.FullName,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser? user = await _userManager.FindByIdAsync(id);

                if (user is null)
                    return BadRequest();
                if (!model.ChangePassword)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.FullName = model.FullName;

                    IdentityResult result = await _userManager.UpdateAsync(user);

                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index));
                }
                else
                {
                    string token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    IdentityResult passwordResult = await _userManager.ResetPasswordAsync(user, token, model.Password!);

                    if (!passwordResult.Succeeded)
                    {
                        foreach (IdentityError error in passwordResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }

                        return View(model);
                    }

                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }
    }
}