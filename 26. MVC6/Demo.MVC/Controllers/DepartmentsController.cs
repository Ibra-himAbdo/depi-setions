using Demo.BLL.Interfaces;
using Demo.DAL.Models;
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
            ValidationResult valdaiton = await _validator.ValidateAsync(department);

            if (valdaiton.IsValid)
            {
                try
                {
                    await _departmentRepository.AddAsync(department);

                    TempData[AppConsts.TosterMessageKey] = JsonSerializer.Serialize(
                        new TosterViewModel()
                        {
                            Content = $"Department Created with 'id': {department.Id}"
                        });

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message + " - " + ex.InnerException?.Message);
                }
            }
            else
            {
                foreach (ValidationFailure? error in valdaiton.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return View(department);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Department? department = await _departmentRepository.GetByIdAsync(id);
            if (department is null)
                return NotFound();

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, Department department)
        {
            if (id != department.Id)
                return BadRequest();

            ValidationResult valdaiton = await _validator.ValidateAsync(department);

            if (valdaiton.IsValid)
            {
                try
                {
                    await _departmentRepository.EditAsync(department);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            else
            {
                foreach (ValidationFailure? error in valdaiton.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return View(department);
        }
    }
}