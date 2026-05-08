namespace Application;

[Route("errors/{code:int}")]
[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController : ControllerBase
{
    public IActionResult Error(int code)
    {
        return code switch
        {
            StatusCodes.Status401Unauthorized => Unauthorized(new ApiResponse(StatusCodes.Status401Unauthorized, "Unauthorized: Authentication is required or has failed.")),
            StatusCodes.Status403Forbidden => Forbid(), //or we can use // StatusCode(StatusCodes.Status403Forbidden,
            // new ApiResponse(StatusCodes.Status403Forbidden, "Forbidden: You do not have permission."));
            _ => NotFound(new ApiResponse(StatusCodes.Status404NotFound, "The requested endpoint or resource was not found.")),
        };
    }
}