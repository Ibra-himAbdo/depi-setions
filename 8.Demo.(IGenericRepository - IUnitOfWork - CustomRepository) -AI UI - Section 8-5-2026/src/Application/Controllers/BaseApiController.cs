namespace Application;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    protected readonly IServiceProvider _serviceProvider;

    public BaseApiController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected async Task<IActionResult?> ValidateRequestModel<T>(T model)
    {
        IValidator<T>? validator = _serviceProvider.GetService<IValidator<T>>();
        if (validator is null) return null;

        var result = await validator.ValidateAsync(model);

        if (result.IsValid) return null;

        List<string>? errors = result.Errors.Select(e => e.ErrorMessage).ToList();

        return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, errors));
    }

    protected IActionResult ReturnResult<T>(Result<T> result, ResponseStatus status = ResponseStatus.Ok)
    {
        return result.IsSuccess ? HandleSuccess(status, result.Value) : HandleError(result.Error);
    }

    protected IActionResult ReturnResult(Result result, ResponseStatus status = ResponseStatus.Ok)
    {
        return result.IsSuccess ? HandleSuccess(status) : HandleError(result.Error);
    }

    private IActionResult HandleSuccess(ResponseStatus status, object? value = null)
    {
        return status switch
        {
            ResponseStatus.Created => Created(string.Empty, value),
            ResponseStatus.NoContent => NoContent(),
            _ => Ok(value)
        };
    }

    private IActionResult HandleError(Error? error)
    {
        if (error is null)
            return StatusCode(500, new ApiResponse(500, "Unknown error"));

        return error.Type switch
        {
            ErrorType.NotFound => NotFound(new ApiResponse(404, error.Description)),
            ErrorType.Validation => BadRequest(new ApiResponse(400, error.Description)),
            ErrorType.Conflict => Conflict(new ApiResponse(409, error.Description)),
            ErrorType.Failure => StatusCode(500, new ApiResponse(500, error.Description)),
            _ => StatusCode(500, new ApiResponse(500, "Unexpected error"))
        };
    }
}

public enum ResponseStatus
{
    Ok, //200
    Created, //201
    NoContent //204
}