using FluentValidation;

namespace Demo.BLL.Validators
{
    public class DepartmentValidator : AbstractValidator<Department>
    {
        private const int maxLength = 100;

        public DepartmentValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty()
                .WithMessage("Name is Required - Department Validator")
                .MaximumLength(maxLength);

            RuleFor(d => d.Code)
                .NotEmpty()
                .WithMessage("Code is Required - Department Validator")
                .MaximumLength(maxLength);

            RuleFor(d => d.DateOfCreation)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Date Of Creation cannot be in the future.");

            RuleFor(d => d.StartDate)
                .NotNull()
                .WithMessage("Start Date cant be empty");

            RuleFor(d => d.EndDate)
                .NotNull()
                .WithMessage("End Date cant be empty");

            RuleFor(d => d.EndDate)
                .GreaterThanOrEqualTo(d => d.StartDate)
                .When(d => d.StartDate.HasValue && d.EndDate.HasValue);
        }

    }
}
