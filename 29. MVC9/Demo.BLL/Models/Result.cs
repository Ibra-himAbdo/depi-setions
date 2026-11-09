
namespace Demo.BLL.Models
{
    public class Result
    {
        public bool IsSuccess { get; }
        public List<Error>? Errors { get; }

        public Result(bool isSuccess, params List<Error>? errors)
        {
            IsSuccess = isSuccess;
            Errors = errors;
        }

        public static Result Success() => new(true, null);
        public static Result Failure(params List<Error> errors) => new(false, errors);

        public static implicit operator Result(Error error) => Failure(error);
        public static implicit operator Result(List<Error> errors) => Failure(errors);
        public static implicit operator Result(FluentValidation.Results.ValidationResult valdaiton)
        {
            List<Error> errors = [];

            foreach (FluentValidation.Results.ValidationFailure? error in valdaiton.Errors)
            {
                errors.Add(Error.Validation(error.PropertyName, error.ErrorMessage));
            }

            return Failure(errors);
        }
    }

    public class Result<T> : Result
    {
        public T? Value { get; }

        public Result(T value) : base(true, null) => Value = value;

        public Result(params List<Error> errors) : base(false, errors) { }

        public static implicit operator Result<T>(Error error) => new(error);
        public static implicit operator Result<T>(List<Error> errors) => new(errors);
        public static implicit operator Result<T>(FluentValidation.Results.ValidationResult valdaiton)
        {
            List<Error> errors = [];

            foreach (FluentValidation.Results.ValidationFailure? error in valdaiton.Errors)
            {
                errors.Add(Error.Validation(error.PropertyName, error.ErrorMessage));
            }

            return new(errors);
        }
    }

    public class Error
    {
        public string? Code { get; set; }
        public ErrorType Type { get; set; }
        public string? Descreiption { get; set; }

        public Error(string? code, ErrorType type, string? descreiption)
        {
            Code = code ?? type.ToString();
            Type = type;
            Descreiption = descreiption;
        }
        public static Error NotFound(string? code = null, string? descreiption = null)
            => new(code, ErrorType.NotFound, descreiption);

        public static Error Validation(string? code = null, string? descreiption = null)
            => new(code, ErrorType.Validation, descreiption);

        public static Error Failure(string? code = null, string? descreiption = null)
            => new(code, ErrorType.Failure, descreiption);
    }

    public enum ErrorType
    {
        Validation,
        NotFound,
        Failure
    }
}
