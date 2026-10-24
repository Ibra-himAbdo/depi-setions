using Demo.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Demo.MVC.Controllers
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var product = new Product()
            {
                Id = 1,
                Name = "Test1",
            };


            return View();
            //return Json(product);

            //return Content("<h1>Hello</h1>","text/html");
            //return Content("<data><product><name>data</name></product></data>", "text/xml");
            //return Content("Hello", "text/palin");

            //return StatusCode(404);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}