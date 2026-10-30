namespace Demo.BLL.Validations
{
    public class DepartmentValidator : AbstractValidator<Department>
    {
        private const int maxLength = 100;

        public DepartmentValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty()
                .WithMessage("Name is required - Department Validator")
                .MaximumLength(maxLength)
                .WithMessage("Name length cant be execed 100 character");

            RuleFor(d => d.Code)
                .NotEmpty()
                .WithMessage("Code is required - Department Validator")
                .MaximumLength(maxLength)
                .WithMessage("Code length cant be execed 100 character");

            RuleFor(d => d.DateOfCreation)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("No Future Date");

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