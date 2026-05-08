namespace Application.Core;

public class Product : BaseSettingEntity
{
    public string? Description { get; set; }
    public string? PictureUrl { get; set; }
    public decimal Price { get; set; }

    public Guid? BrandId { get; set; }
    public ProductBrand? Brand { get; set; }

    public Guid? CategoryId { get; set; }
    public ProductCategory? Category { get; set; }
}