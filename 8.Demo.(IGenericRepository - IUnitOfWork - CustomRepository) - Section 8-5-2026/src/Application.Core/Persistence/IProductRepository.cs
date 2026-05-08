namespace Application.Core;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<IReadOnlyList<Product>> GetLast5ProductsAsync(CancellationToken cancellationToken = default);
    Task<string> GetLast5ProductsAsJsonAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Product>> GetProductsWithBrandsAndCategoryAsync(CancellationToken cancellationToken = default);
    Task<PageMetaData<Product>> GetProductsWithBrandsAndCategoryAsync(PageMetaData<Product> pageMeta, CancellationToken cancellationToken = default);
    Task<Product?> GetSingleProductWithBrandsAndCategoryAsync(Guid id, CancellationToken cancellationToken = default);
}
