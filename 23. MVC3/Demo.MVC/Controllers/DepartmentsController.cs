using Demo.DAL.Data;
using Demo.DAL.Models;

namespace Demo.MVC.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly AppDbContext _dbContext;

        public DepartmentsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View(_dbContext.Departments.ToList());
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
                    _dbContext.Departments.Add(department);
                    _dbContext.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View();
        }

        public IActionResult Edit(int id)
        {
            var department = _dbContext.Departments.Find(id);
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

            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Departments.Update(department);
                    _dbContext.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View();
        }
    }
}