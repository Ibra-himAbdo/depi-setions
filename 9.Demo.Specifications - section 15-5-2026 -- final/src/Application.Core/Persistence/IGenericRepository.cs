namespace Application.Core;

public interface IGenericRepository<T> where T : class, IEntity
{
    //read
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<T?> GetByIdAsync(object id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<T>> FindAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    //write

    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);

    // helpers
    Task<int> GetCountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    Task<TResult> GetMaxAsync<TResult>(Expression<Func<T, TResult>> selector, CancellationToken cancellationToken = default);

    Task<TResult> GetMinAsync<TResult>(Expression<Func<T, TResult>> selector, CancellationToken cancellationToken = default);

    // Specifications
    Task<IReadOnlyList<T>> GetAllWithSpecificationsAsync(ISpecifications<T> specifications, CancellationToken cancellationToken = default);

    Task<T?> GetByIdWithSpecificationsAsync(ISpecifications<T> specifications, CancellationToken cancellationToken = default);

    Task<int> GetCountAsync(ISpecifications<T> specifications, CancellationToken cancellationToken = default);

    // Projection 
    Task<IReadOnlyList<TResult?>> ProjectAsync<TResult>(Expression<Func<T, TResult?>> selector, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TResult?>> ProjectToAsync<TResult>(CancellationToken cancellationToken = default);
}