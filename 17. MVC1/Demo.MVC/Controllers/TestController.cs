using Microsoft.AspNetCore.Mvc;

namespace Demo.MVC.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index(int id, string? name)
        {
            if (name is null || id == 0)
            {
                return BadRequest("u cant send empty Id or name");
            }
            return Content($"{id} - {name}");
        }

        public IActionResult Data(int[] ids)
        {
            if (ids.Length == 0)
            {
                return BadRequest("u cant send empty ids");
            }
            return Content(string.Join(", ", ids));
        }

        //[HttpPost]
        //public IActionResult Complex([FromBody] Product? product)
        //{
        //    if (product is null)
        //    {
        //        return BadRequest("u cant send empty product");
        //    }
        //    return Content($"{product.Id} - {product.Name}");
        //}

        public IActionResult Complex([FromForm] Product? product)
        {
            if (product is null)
            {
                return BadRequest("u cant send empty product");
            }
            return Content($"{product.Id} - {product.Name}");
        }
    }
}
