namespace Demo.BLL.Models;

public class Result
{
    public bool IsSuccess { get; set; }
    public List<Error>? Errors { get; set; }

    public Result(bool isSuccess, params List<Error>? errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public static Result Success() => new(true, null);

    public static Result Failure(params List<Error> errors) => new(false, errors);

    public static implicit operator Result(List<Error> errors) => Failure(errors);

    public static implicit operator Result(Error error) => Failure(error);

    public static implicit operator Result(FluentValidation.Results.ValidationResult result)
    {
        List<Error> errors = [];
        foreach (var error in result.Errors)
        {
            errors.Add(Error.Validation(error.PropertyName, error.ErrorMessage));
        }

        return Failure(errors);
    }
}

public class Result<T> : Result
{
    public T? Value { get; set; }

    public Result(T value) : base(true, null)
    {
        Value = value;
    }
    public Result(params List<Error> errors) : base(false, errors) { }

    public static implicit operator Result<T>(List<Error> errors) => new(errors);

    public static implicit operator Result<T>(Error error) => new(error);

    public static implicit operator Result<T>(FluentValidation.Results.ValidationResult result)
    {
        List<Error> errors = [];
        foreach (var error in result.Errors)
        {
            errors.Add(Error.Validation(error.PropertyName, error.ErrorMessage));
        }

        return new(errors);
    }
}

public class Error
{
    public string? Code { get; set; }
    public string? Descreption { get; set; }
    public ErrorType Type { get; set; }

    public Error(string? code, ErrorType type, string? descreption)
    {
        Code = code ?? type.ToString();
        Type = type;
        Descreption = descreption;
    }

    public static Error NotFound(string? code = null, string? descreption = null) => new(code, ErrorType.NotFound, descreption);

    public static Error Validation(string? code = null, string? descreption = null) => new(code, ErrorType.Validation, descreption);

    public static Error Failure(string? code = null, string? descreption = null) => new(code, ErrorType.Failure, descreption);
}

public enum ErrorType
{
    Validation,
    NotFound,
    Failure
}