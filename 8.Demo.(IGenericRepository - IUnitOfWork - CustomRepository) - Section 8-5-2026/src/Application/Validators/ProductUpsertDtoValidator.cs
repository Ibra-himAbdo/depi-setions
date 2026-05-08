namespace Application;

public class ProductUpsertDtoValidator : AbstractValidator<ProductUpsertDto>
{
    public ProductUpsertDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(dto => dto.BrandId)
            .NotEmpty().WithMessage("Brand is required.");

        RuleFor(dto => dto.CategoryId)
            .NotEmpty().WithMessage("Category is required.");

        RuleFor(dto => dto.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0.");
    }
}

