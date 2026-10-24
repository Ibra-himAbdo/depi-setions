using Demo.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Demo.MVC.Controllers
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public IFormFile? Image { get; set; }
    }
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();

            // json
            //var product = new Product()
            //{
            //    Id = 5,
            //    Name = "test"
            //};
            //return new JsonResult(product);

            //return new ContentResult()
            //{
            //    Content ="<h1>Hello</h1>",
            //    //ContentType="text/html"
            //    ContentType="text/plain"
            //};

            //return RedirectToAction(nameof(Privacy));

            //return StatusCode(201);
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