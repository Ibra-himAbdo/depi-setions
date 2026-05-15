
using Microsoft.AspNetCore.Authorization;

namespace Application;

public class TestsController : BaseApiController
{
    public TestsController(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello from the API!");
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        if(int.TryParse(id, out int numericId))
            return Ok($"You requested the resource with ID: {numericId}");
        
        return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Invalid ID format. ID must be a numeric value."));
    }
    
    [HttpGet("throw")]
    public IActionResult ThrowException()
    {
        throw new NotImplementedException("This is a test");
    }

    [HttpGet("test-authorize")]
    [Authorize]
    public IActionResult Authorize()
    {
        return Ok("You are Authorized");
    }
}
