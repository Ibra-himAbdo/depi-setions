namespace Application;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.Identifier)
            .NotEmpty().WithMessage("Username or Email is required");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(Resource.PasswordIsRequired)
            .MinimumLength(6).WithMessage("Password must be at least 6 characters")
            .Matches("[A-Z]").WithMessage("Password must contain at least 1 uppercase letter")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least 1 special character");
    }
}