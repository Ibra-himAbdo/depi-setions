using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Demo.MVC.Controllers;

[Authorize(Roles = AppRoles.Admin)]
public class AdminsController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IDepartmentRepository _departmentRepository;

    public AdminsController(UserManager<AppUser> userManager, IDepartmentRepository departmentRepository)
    {
        _userManager = userManager;
        _departmentRepository = departmentRepository;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _userManager.Users.ToListAsync());
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Deprts = await _departmentRepository.GetAllAsync();

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
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
            UserName = model.Email,
            DepartmentId = model.Department,
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

        if (model.SelectedRoles is not null && model.SelectedRoles.Count > 0)
        {
            await _userManager.AddToRolesAsync(appUser, model.SelectedRoles);
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
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel model)
    {
        if (ModelState.IsValid)
        {
            AppUser? user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return BadRequest();

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

        return View(model);
    }

    public async Task<IActionResult> ChangePassword(string id)
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
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword([FromRoute] string id, UserViewModel model)
    {
        AppUser? user = await _userManager.FindByIdAsync(id);

        if (user is null)
            return BadRequest();

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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string id)
    {
        AppUser? user = await _userManager.FindByIdAsync(id);

        if (user is null) return BadRequest();

        IdentityResult result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            ViewBag.Errors = result.Errors;
        }
        return RedirectToAction(nameof(Index));
    }
}