namespace Application.Core;

public class ProductCountSpecifications : BaseSpecifications<Product>
{
    public ProductCountSpecifications(ProductSpecParams specParams) 
        : base(p =>
            (
                string.IsNullOrWhiteSpace(specParams.Search) ||
                (p.Name != null && p.Name.ToUpper().Contains(specParams.Search)) ||
                (p.NameSecondLanguage != null && p.NameSecondLanguage.ToUpper().Contains(specParams.Search))
            ) &&
            (!specParams.BrandId.HasValue || p.BrandId == specParams.BrandId) &&
            (!specParams.CategoryId.HasValue || p.CategoryId == specParams.CategoryId)
        )
    {
    }
}