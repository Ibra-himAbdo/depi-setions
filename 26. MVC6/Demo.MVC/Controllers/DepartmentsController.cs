using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Data;
using Demo.DAL.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        public IActionResult Index()
        {
            return View(_departmentRepository.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department department)
        {
            ValidationResult valdaiton = _validator.Validate(department);

            if (valdaiton.IsValid)
            {
                try
                {
                    _departmentRepository.Add(department);

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

        public IActionResult Edit(int id)
        {
            var department = _departmentRepository.GetById(id);
            if (department is null)
                return NotFound();

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Department department)
        {
            if (id != department.Id)
                return BadRequest();

            ValidationResult valdaiton = _validator.Validate(department);

            if (valdaiton.IsValid)
            {
                try
                {
                    _departmentRepository.Edit(department);

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