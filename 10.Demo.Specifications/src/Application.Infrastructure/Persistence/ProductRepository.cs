using System.Text.Json;

namespace Application.Infrastructure;

internal class ProductRepository : GenericRepository<Product>, IProductRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> GetLast5ProductsAsJsonAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyList<Product> products = await GetLast5ProductsAsync(cancellationToken);

        return JsonSerializer.Serialize(products);
    }

    public async Task<IReadOnlyList<Product>> GetLast5ProductsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Product>()
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .OrderByDescending(p => p.Id)
            .Take(5)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Product>> GetProductsWithBrandsAndCategoryAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Product>()
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetSingleProductWithBrandsAndCategoryAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Product>()
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}
