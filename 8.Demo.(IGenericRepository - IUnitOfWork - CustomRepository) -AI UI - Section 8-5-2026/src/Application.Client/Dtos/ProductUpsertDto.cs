namespace Application.Client;

public record ProductUpsertDto(
    string Name,
    string NameSecondLanguage,
    string Description,
    string PictureUrl,
    decimal Price,
    Guid BrandId,
    Guid CategoryId
);
