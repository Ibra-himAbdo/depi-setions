namespace Application.Core;

public record Result
{
    public bool IsSuccess { get; }
    public Error? Error { get; }

    public Result(bool isSuccess, Error? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, null);
    public static Result Failure(Error error) => new(false, error);

    public static implicit operator Result(Error error) => Failure(error);
}

public record Result<T> : Result
{
    public T? Value { get; }

    private Result(T value) : base(true, null) => Value = value;
    private Result(Error error) : base(false, error) { }

    public static implicit operator Result<T>(T value) => new(value);

    public static implicit operator Result<T>(Error error) => new(error);
}

public enum ErrorType
{
    NotFound,
    Validation,
    Failure,
    Conflict
}

public record Error
{
    public string? Code { get; init; }
    public ErrorType Type { get; init; }
    public string? Description { get; init; }

    public Error(string? code, ErrorType type, string? description)
    {
        Code = code ?? type.ToString();
        Type = type;
        Description = description;
    }

    public static Error NotFound(string? code = null, string? description = null) => new(code, ErrorType.NotFound, description);

    public static Error Validation(string? code = null, string? description = null) => new(code, ErrorType.Validation, description);

    public static Error Failure(string? code = null, string? description = null) => new(code, ErrorType.Failure, description);

    public static Error Conflict(string? code = null, string? description = null) => new(code, ErrorType.Conflict, description);
}