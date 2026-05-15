namespace Application.Core;

public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
{
    public ProductWithBrandAndCategorySpecifications() : base()
    {
        AddIncludes();
    }

    public ProductWithBrandAndCategorySpecifications(ProductSpecParams specParams) 
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
        if (specParams.WithIncludes)
        {
            AddIncludes();
        }

        if (!string.IsNullOrWhiteSpace(specParams.Sort))
        {
            switch (specParams.Sort)
            {
                case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break;
                case "priceDesc":
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Name!);
                    break;
            }
        }
        else
        {
            AddOrderBy(p => p.Name!);
        }

        ApplyPagination(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
    }

    public ProductWithBrandAndCategorySpecifications(Guid id) : base(e => e.Id == id)
    {
        AddIncludes();
    }

    private void AddIncludes()
    {
        Includes.Add(P => P.Brand!);
        Includes.Add(P => P.Category!);
    }
}