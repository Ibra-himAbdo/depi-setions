namespace Application.Validators;

public class ConfirmEmailDtoValidation : AbstractValidator<ConfirmEmailDto>
{
    public ConfirmEmailDtoValidation()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required")
            .NotNull().WithMessage("User ID cannot be null");

        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token is required")
            .NotNull().WithMessage("Token cannot be null");
    }
}