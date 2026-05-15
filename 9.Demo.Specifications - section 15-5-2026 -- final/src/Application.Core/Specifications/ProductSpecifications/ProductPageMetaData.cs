namespace Application.Core;

public class ProductPageMetaData : PageMetaData<Product>
{
    public Guid? BrandId { get; set; }
    public Guid? CategoryId { get; set; }

    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}