namespace Application.Core;

public class ProductCountSpecifications : BaseSpecifications<Product>
{
    public ProductCountSpecifications(ProductPageMetaData pageMetaData)
       : base(p =>
           (string.IsNullOrWhiteSpace(pageMetaData.Search) ||
               (pageMetaData.IsRtl ? p.NormalizedNameSecondLanguage : p.NormalizedName)!.Contains(pageMetaData.Search!)) &&
           (!pageMetaData.BrandId.HasValue || p.BrandId == pageMetaData.BrandId) &&
           (!pageMetaData.CategoryId.HasValue || p.CategoryId == pageMetaData.CategoryId) &&
            (!pageMetaData.MaxPrice.HasValue || p.Price <= pageMetaData.MaxPrice) &&
            (!pageMetaData.MinPrice.HasValue || p.Price >= pageMetaData.MinPrice)
       )
    {
    }
}