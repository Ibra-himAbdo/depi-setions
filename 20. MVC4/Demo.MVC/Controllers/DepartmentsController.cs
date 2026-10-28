using Microsoft.EntityFrameworkCore;

namespace Demo.MVC.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly AppDbContext _context;

        public DepartmentsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Departments.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department department)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Departments.Add(department);
                    _context.SaveChanges();

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
