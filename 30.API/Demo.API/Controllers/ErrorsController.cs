using Demo.API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{
    [Route("errors/{code:int}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        public IActionResult Error(int code)
        {
            return code switch
            {
                StatusCodes.Status401Unauthorized => Unauthorized(new ApiResponse(StatusCodes.Status401Unauthorized, "Unauthorized: Authintaiont is requered")),
                _ => NotFound(new ApiResponse(StatusCodes.Status404NotFound, $"The requsted endpoint or resurces was not found."))
            };
        }
    }
}
