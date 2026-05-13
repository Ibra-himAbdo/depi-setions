using Application.Core.Models;

namespace Application.Core;

public class ProductSpecificationsWithCategoryAndBrand : BaseSpecifications<Product>
{
    public ProductSpecificationsWithCategoryAndBrand(ProductPageMetaData pageMetaData)
        : base(p =>
            (string.IsNullOrWhiteSpace(pageMetaData.Search) ||
                (pageMetaData.IsRtl ? p.NormalizedNameSecondLanguage : p.NormalizedName)!.Contains(pageMetaData.Search!)) &&
            (!pageMetaData.BrandId.HasValue || p.BrandId == pageMetaData.BrandId) &&
            (!pageMetaData.CategoryId.HasValue || p.CategoryId == pageMetaData.CategoryId)
        )
    {
        AddIncludes();

        if (!string.IsNullOrWhiteSpace(pageMetaData.SortBy))
        {
            ProductSort enumAsString = Enum.Parse<ProductSort>(pageMetaData.SortBy, true);

            switch (enumAsString)
            {
                case ProductSort.NameAsc:
                    AddOrderBy(p => p.NormalizedName!);
                    break;

                case ProductSort.NameDesc:
                    AddOrderByDescending(p => p.NormalizedName!);
                    break;

                case ProductSort.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;

                case ProductSort.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;

                case ProductSort.BrandAsc:
                    AddOrderBy(p => p.Brand!.Name!);
                    break;

                case ProductSort.BrandDesc:
                    AddOrderByDescending(p => p.Brand!.Name!);
                    break;

                default:
                    AddOrderBy(p => p.CreatedAt);
                    break;
            }
        }
        else
        {
            AddOrderBy(p => p.CreatedAt);
        }

        ApplPagination((pageMetaData.CurrentPageIndex - 1) * pageMetaData.CurrentPageSize, pageMetaData.CurrentPageSize);
    }

    public ProductSpecificationsWithCategoryAndBrand(Guid id)
        : base(e => e.Id == id)
    {
        AddIncludes();
    }

    private void AddIncludes()
    {
        Includes ??= new();

        Includes.Add(e => e.Brand);
        Includes.Add(e => e.Category);
    }
}