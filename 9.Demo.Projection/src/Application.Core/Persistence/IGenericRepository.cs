namespace Application.Core;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<T?> GetByIdAsync(object id, CancellationToken cancellationToken = default);

    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);

    Task<int> GetCountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<T>> FindAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    // 1. Projection
    Task<IReadOnlyList<TResult>> ProjectAsync<TResult>(Expression<Func<T, TResult>> selector, CancellationToken cancellationToken = default);

    // 2. Mapster Projection (convenience)
    Task<IReadOnlyList<TDto>> ProjectToAsync<TDto>(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default);
}