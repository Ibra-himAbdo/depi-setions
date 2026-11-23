using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpGet("AuthorizeEndpoint")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult AuthorizeEndpoint()
        {
            var user = User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier);
            return Ok("hi from AuthorizeEndpoint");
        }
    }
}
