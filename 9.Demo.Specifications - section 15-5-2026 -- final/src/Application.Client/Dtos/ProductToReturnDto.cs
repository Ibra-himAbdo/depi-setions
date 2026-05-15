namespace Application.Client;

public record ProductToReturnDto(
    Guid Id,
    string Name,
    string NameSecondLanguage,
    string Description,
    string PictureUrl,
    decimal Price,
    string Brand,
    string BrandNameSecondLanguage,
    string Category,
    string CategoryNameSecondLanguage
);