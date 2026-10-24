using Demo.View.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Demo.View.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(int id)
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult ContuctUs()
        {
            return View();
        }
    }
}
