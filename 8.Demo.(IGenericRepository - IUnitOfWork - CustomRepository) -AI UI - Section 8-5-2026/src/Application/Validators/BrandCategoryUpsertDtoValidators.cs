namespace Application;

public class BrandUpsertDtoValidator : AbstractValidator<BrandUpsertDto>
{
    public BrandUpsertDtoValidator()
    {
        RuleFor(d => d.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(d => d.NameSecondLanguage)
            .NotEmpty().WithMessage("Name (second language) is required.");
    }
}

public class CategoryUpsertDtoValidator : AbstractValidator<CategoryUpsertDto>
{
    public CategoryUpsertDtoValidator()
    {
        RuleFor(d => d.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(d => d.NameSecondLanguage)
            .NotEmpty().WithMessage("Name (second language) is required.");
    }
}
