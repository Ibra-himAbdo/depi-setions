namespace Demo.Core.Abstractions;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAllAsync();

    Task<T?> GetById(int id);

    Task<int> AddAsync(T entity);

    Task<int> UpdateAsync(T entity);

    Task<int> DeleteAsync(T entity);

    Task<int> DeleteByIdAsync(int id);

    // we need it untile we implement unit of work
    //Task<int> CommitAsync();
}