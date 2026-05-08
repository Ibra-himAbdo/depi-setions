namespace Application;

public class ApiResponse
{
    public int StatusCode { get; set; }
    public List<string>? Errors { get; set; }

    public ApiResponse(int statusCode, List<string> errors)
    {
        StatusCode = statusCode;
        Errors = errors;
    }

    public ApiResponse(int statusCode, string? error = null)
    {
        StatusCode = statusCode;
        
        Errors ??= new();
        Errors.Add(error ?? GetDefaultMessageForStatusCode(statusCode));
    }
    
    public ApiResponse(int statusCode, string error, string? details)
    {
        StatusCode = statusCode;
        Errors ??= new();
        Errors.Add($"{error}: {details}");
    }

    private string GetDefaultMessageForStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "The request is invalid.",
            401 => "Authentication is required to access this resource.",
            403 => "You do not have permission to perform this action.",
            404 => "The requested resource was not found.",
            405 => "The HTTP method is not allowed for this endpoint.",
            409 => "A conflict occurred with the current state of the resource.",
            422 => "The request was well-formed but contains semantic errors.",
            429 => "Too many requests. Please try again later.",
            500 => "An internal server error occurred. Please try again later.",
            503 => "The service is temporarily unavailable.",
            _ => string.Empty
        };
    }
}