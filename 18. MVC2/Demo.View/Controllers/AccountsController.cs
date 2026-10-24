using Microsoft.AspNetCore.Mvc;

namespace Demo.View.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
