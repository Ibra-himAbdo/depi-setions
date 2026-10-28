using Demo.BLL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Demo.MVC.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
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
            if (ModelState.IsValid)
            {
                try
                {
                    _departmentRepository.Add(department);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message + " - " + ex.InnerException?.Message);
                }
            }

            return View(department);
        }

        public IActionResult Edit(int id)
        {
            Department? department = _departmentRepository.GetById(id);
            if (department is null) return NotFound();

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Department department)
        {
            if (id != department.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    _departmentRepository.Update(department);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message + " - " + ex.InnerException?.Message);
                }
            }

            return View();
        }

    }
}
