using Demo.BLL.Interfaces;
using Demo.BLL.Services;
using Demo.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Demo.MVC.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]
    public class AdminsController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IFileService _fileService;

        public AdminsController(UserManager<AppUser> userManager, IDepartmentRepository departmentRepository, IFileService fileService)
        {
            _userManager = userManager;
            _departmentRepository = departmentRepository;
            _fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            List<AppUser> user = await _userManager.Users.ToListAsync();
            return View(user);
        }

        public async Task<IActionResult> Create()
        {
            //ViewBag.Departments = await _departmentRepository.GetAllAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var fileResult = await _fileService.UploadFile(model.Image);

                if(!fileResult.IsSuccess || fileResult.Value is null)
                {
                   return HandelErrors(fileResult.Errors) ?? View(model);
                }
                // upload start
                //if (model.Image is null || model.Image.Length == 0)
                //{
                //    ModelState.AddModelError(nameof(UserViewModel.Image), "Please Slect an image.");
                //    return View(model);
                //}

                //IEnumerable<string> allowedExtenions = [".png", ".jpg", ".jpeg", ".webp"];

                //var fileExtenion = Path.GetExtension(model.Image.FileName).ToLower();

                //if(!allowedExtenions.Contains(fileExtenion))
                //{
                //    ModelState.AddModelError(nameof(UserViewModel.Image), "Invalid file fromat.");
                //    return View(model);
                //}
                //const string imageFolderName = "images";
                //string folderPath = Path.Combine(_environment.WebRootPath, imageFolderName);

                //Directory.CreateDirectory(folderPath);
                //// yousse.png
                //// 5464654-65145-6546456-654565.png
                //// 5464654651456546456654565.png ("N")
                //string fileName = Guid.NewGuid().ToString("N") + fileExtenion;

                //string filePath = Path.Combine(folderPath, fileName);

                //using FileStream stream = new FileStream(filePath, FileMode.Create);

                //await model.Image.CopyToAsync(stream);
                // upload ended

                AppUser user = new AppUser()
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    UserName = model.Email,
                    DepartmentId = model.Department,
                    ImagePath = fileResult.Value,
                };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password!);

                if (result.Succeeded)
                {
                    if (model.SelectedRoles is not null && model.SelectedRoles.Count > 0)
                    {
                        await _userManager.AddToRolesAsync(user, model.SelectedRoles);
                    }

                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null) return NotFound();

            IdentityResult result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                ViewBag.Errors = result.Errors;
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword([FromRoute] string id, ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user is null) return NotFound();

                string token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword!);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
    }
}