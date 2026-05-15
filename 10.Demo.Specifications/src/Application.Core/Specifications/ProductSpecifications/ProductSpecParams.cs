namespace Application.Core;

public record ProductSpecParams : BaseSpecificationParams
{
    public Guid? BrandId { get; set; }
    public Guid? CategoryId { get; set; }
}
