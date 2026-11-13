using Demo.API.Errors;
using Microsoft.AspNetCore.Http;
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
                StatusCodes.Status401Unauthorized => Unauthorized(new ApiResponse(StatusCodes.Status401Unauthorized, "Unauthorized: U neeed to be Authintacated")),
                _ => NotFound(new ApiResponse(StatusCodes.Status404NotFound, "The reqsted endpoint or resurses was not found"))
            };
        }
    }
}
