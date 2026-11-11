using Demo.BLL.Interfaces;
using Demo.BLL.Models;
using Demo.BLL.Services;
using Demo.MVC.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace Demo.MVC.Controllers
{
    [Authorize]
    public class DepartmentsController : BaseController
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDepretmetService _depretmetService;

        public DepartmentsController(IDepartmentRepository departmentRepository, IDepretmetService depretmetService)
        {
            _departmentRepository = departmentRepository;
            _depretmetService = depretmetService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            List<Department> data = await _departmentRepository.GetAllAsync();
            return View(data);
        }

        [Authorize(Roles = $"{AppRoles.Admin},{AppRoles.Manager}")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            Result result = await _depretmetService.CreateDepartmentAsync(department);

            if (result.IsSuccess)
            {
                TempData[AppConsts.TossterMessageKey] = JsonSerializer.Serialize(new TosterViewModel { Content = $"Departmet with name {department.Name} Added and has Id: '{department.Id}'!" });

                return RedirectToAction(nameof(Index));
            }

            //var errorResult = HandelErrors(result.Errors);
            //if(errorResult is not null)
            //{
            //    return errorResult;
            //}

            //return View(department);

            return HandelErrors(result.Errors) ?? View(department);
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
            if (id != department.Id) return BadRequest();

            Result result = await _depretmetService.EditDepartmentAsync(department);

            if (result.IsSuccess)
            {
                TempData[AppConsts.TossterMessageKey] = JsonSerializer.Serialize(new TosterViewModel { Content = $"Departmet with name {department.Name} Edited!" });

                return RedirectToAction(nameof(Index));
            }

            return HandelErrors(result.Errors) ?? View(department);
        }

    }
}
