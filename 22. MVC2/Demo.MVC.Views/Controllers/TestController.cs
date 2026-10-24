using Microsoft.AspNetCore.Mvc;

namespace Demo.MVC.Views.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
