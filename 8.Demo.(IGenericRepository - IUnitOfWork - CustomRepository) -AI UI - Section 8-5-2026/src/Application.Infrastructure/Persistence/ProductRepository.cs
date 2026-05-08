using Application.Core.Models;
using System.Globalization;
using System.Text.Json;

namespace Application.Infrastructure;

internal class ProductRepository(ApplicationDbContext dbContext)
    : GenericRepository<Product>(dbContext), IProductRepository
{
    public async Task<string> GetLast5ProductsAsJsonAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyList<Product> products = await GetLast5ProductsAsync(cancellationToken);

        return JsonSerializer.Serialize(products);
    }

    public async Task<IReadOnlyList<Product>> GetLast5ProductsAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<Product>()
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .OrderByDescending(p => p.Id)
            .Take(5)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Product>> GetProductsWithBrandsAndCategoryAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<Product>()
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<PageMetaData<Product>> GetProductsWithBrandsAndCategoryAsync(PageMetaData<Product> pageMeta, CancellationToken cancellationToken = default)
    {
        bool isRtl = CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft;
        
        IQueryable<Product> query = dbContext.Set<Product>()
            .AsNoTracking().AsQueryable();


        if (!string.IsNullOrWhiteSpace(pageMeta.Search))
        {
            query = query.Where(p => (isRtl ? p.NormalizedNameSecondLanguage : p.NormalizedName)!.Contains(pageMeta.Search));
        }

        if (pageMeta.BrandId.HasValue)
        {
            query = query.Where(p => p.BrandId == pageMeta.BrandId.Value);
        }

        if (pageMeta.CategoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == pageMeta.CategoryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(pageMeta.SortBy))
        {
            ProductSort enumAsString = Enum.Parse<ProductSort>(pageMeta.SortBy, true);
            query = enumAsString switch
            {
                ProductSort.NameAsc => query.OrderBy(p => p.NormalizedName),
                ProductSort.NameDesc => query.OrderByDescending(p => p.NormalizedName),
                ProductSort.PriceAsc => query.OrderBy(p => p.Price),
                ProductSort.PriceDesc => query.OrderByDescending(p => p.Price),
                ProductSort.BrandAsc => query.OrderBy(p => p.Brand!.Name),
                ProductSort.BrandDesc => query.OrderByDescending(p => p.Brand!.Name),
                _ => query.OrderBy(p => p.Id)
            };
        }
        else
        {
            query = query.OrderBy(p => p.Id);
        }


        int totalCount = await query.CountAsync();

        query = query
            .Skip((pageMeta.CurrentPageIndex - 1) * pageMeta.CurrentPageSize)
            .Take(pageMeta.CurrentPageSize);

        List<Product> data = await query
            .Include(p => p.Brand)
            .Include(p => p.Category).ToListAsync(cancellationToken);

        return new()
        {
            Data = data,
            CurrentPageIndex = pageMeta.CurrentPageIndex,
            CurrentPageSize = pageMeta.CurrentPageSize,
            TotalItemsInDb = totalCount,
            Search = pageMeta.Search,
            SortBy = pageMeta.SortBy,
            BrandId = pageMeta.BrandId,
            CategoryId = pageMeta.CategoryId
        };
    }

    public async Task<Product?> GetSingleProductWithBrandsAndCategoryAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<Product>()
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}