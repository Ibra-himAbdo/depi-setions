using Demo.API.Errors;
using System.Text.Json;

namespace Demo.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _environment;

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.ContentType = "application/Problem+json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            ApiResponse apiResponse = _environment.IsDevelopment() ?
                new(StatusCodes.Status500InternalServerError, ex.Message, ex.StackTrace) :
                new(StatusCodes.Status500InternalServerError, "oops, There is an error!!");

            string json = JsonSerializer.Serialize(apiResponse, _jsonSerializerOptions);

            await context.Response.WriteAsync(json);
        }
    }

}
