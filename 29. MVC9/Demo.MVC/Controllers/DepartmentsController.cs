using Demo.BLL.Interfaces;
using Demo.BLL.Models;
using Demo.BLL.Services;
using Demo.DAL.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace Demo.MVC.Controllers
{
    [Authorize]
    public class DepartmentsController : BaseController
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IValidator<Department> _validator;
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(
            IDepartmentRepository departmentRepository,
            IValidator<Department> validator,
            IDepartmentService departmentService)
        {
            _departmentRepository = departmentRepository;
            _validator = validator;
            _departmentService = departmentService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            List<Department> data = await _departmentRepository.GetAllAsync();

            return View(data);
        }

        [Authorize(Roles = AppRoles.Admin)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            Result result = await _departmentService.CreateDepartmentAsync(department);
            if (result.IsSuccess)
            {
                TempData[AppConsts.TosterMessageKey] = JsonSerializer.Serialize(new TosterViewModel() { Content = $"Department Created with 'id': {department.Id}" });

                return RedirectToAction(nameof(Index));
            }

            //IActionResult? errorResult = HandelErrors(result.Errors);
            //if(errorResult is not null) return errorResult;

            //return View(department);

            return HandelErrors(result.Errors) ?? View(department);
        }


        [Authorize(Roles = $"{AppRoles.Manger},{AppRoles.Admin}")] // admin or manager
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
            if (id != department.Id) return BadRequest();

            var result = await _departmentService.UpdateDepartmentAsync(department);
            if (result.IsSuccess) return RedirectToAction(nameof(Index));

            return HandelErrors(result.Errors) ?? View(department);
        }
    }
}