using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            throw new NotImplementedException();
        }

        [HttpGet("testAuthorize")]
        [Authorize]
        public IActionResult Authorize()
        {
            return Ok(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value);
        }
    }
}
