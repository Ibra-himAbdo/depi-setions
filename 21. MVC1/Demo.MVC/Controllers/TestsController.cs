using Microsoft.AspNetCore.Mvc;

namespace Demo.MVC.Controllers
{

    public class TestsController : Controller
    {
        [HttpPost]
        public IActionResult Index([FromForm] Product product)
        {
            if (product is null)
                return BadRequest("Product cant be null");
            return Content($"{product.Id} - {product.Name}","text/plain");
        }
    }
}
