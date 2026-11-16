using Demo.API.Errors;
using System.Text.Json;

namespace Demo.API.Middelwares
{
    public class ExceptionsMiddelware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionsMiddelware> _logger;
        private readonly IHostEnvironment _environment;
        private readonly JsonSerializerOptions _options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public ExceptionsMiddelware(RequestDelegate next, ILogger<ExceptionsMiddelware> logger, IHostEnvironment environment)
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
                    new(StatusCodes.Status500InternalServerError, "oops, An Error occured");

                string json = JsonSerializer.Serialize(apiResponse, _options);

                await context.Response.WriteAsync(json);
            }

        }
    }
}
