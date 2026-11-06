using Demo.BLL.Interfaces;
using Demo.MVC.Models;
using FluentValidation;
using FluentValidation.Results;
using System.Text.Json;

namespace Demo.MVC.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IValidator<Department> _validator;

        public DepartmentsController(IDepartmentRepository departmentRepository, IValidator<Department> validator)
        {
            _departmentRepository = departmentRepository;
            _validator = validator;
        }

        public async Task<IActionResult> Index()
        {
            List<Department> data = await _departmentRepository.GetAllAsync();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            ValidationResult result = _validator.Validate(department);

            if (result.IsValid)
            {
                try
                {
                    _ = await _departmentRepository.AddAsync(department);

                    TempData[AppConsts.TossterMessageKey] =
                        JsonSerializer.Serialize(new TosterViewModel
                        {
                            Content = $"Departmet with name {department.Name} Added and has Id: '{department.Id}'!"
                        });

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message + " - " + ex.InnerException?.Message);

                    TempData[AppConsts.TossterMessageKey] = JsonSerializer.Serialize(new TosterViewModel
                    {
                        Content = ex.Message + " - " + ex.InnerException?.Message,
                        Type = TosterType.Error
                    });
                }
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return View(department);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Department? department = await _departmentRepository.GetByIdAsync(id);
            if (department is null) return NotFound();

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, Department department)
        {
            if (id != department.Id)
                return BadRequest();

            ValidationResult result = _validator.Validate(department);

            if (result.IsValid)
            {
                try
                {
                    _ = await _departmentRepository.UpdateAsync(department);

                    TempData[AppConsts.TossterMessageKey] =
                        JsonSerializer.Serialize(new TosterViewModel
                        {
                            Content = $"Departmet with name {department.Name} Edited!"
                        });

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message + " - " + ex.InnerException?.Message);
                    TempData[AppConsts.TossterMessageKey] = JsonSerializer.Serialize(new TosterViewModel
                    {
                        Content = ex.Message + " - " + ex.InnerException?.Message,
                        Type = TosterType.Error
                    });
                }
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return View(department);
        }

    }
}
