using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TestsController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            throw new NotImplementedException();
        }
    }
}
