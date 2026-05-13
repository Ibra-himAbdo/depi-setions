namespace Application.Core;

public interface IGenericRepository<T> where T : class , IEntity
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
    Task<int> CountAsync(CancellationToken cancellationToken = default);

    Task<int> GetCountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
}