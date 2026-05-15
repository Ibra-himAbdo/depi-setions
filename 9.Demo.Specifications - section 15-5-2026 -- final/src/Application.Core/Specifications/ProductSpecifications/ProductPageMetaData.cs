namespace Application.Core;

public class ProductPageMetaData : PageMetaData<Product>
{
    public Guid? BrandId { get; set; }
    public Guid? CategoryId { get; set; }
}