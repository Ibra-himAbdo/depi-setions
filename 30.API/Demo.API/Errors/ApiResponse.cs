namespace Demo.API.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public List<string>? Errors { get; set; }

        public ApiResponse() {}

        public ApiResponse(int statusCode, params List<string> errors)
        {
            StatusCode = statusCode;
            Errors = errors;
        }

        public ApiResponse(int statusCode, string error, string details)
        {
            StatusCode = statusCode;
            Errors ??= new();
            Errors.Add($"{error} : {details}");
        }
    }
}
