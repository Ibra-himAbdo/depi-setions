namespace Demo.Core.Abstractions;

public interface IGenericRepository<T> : IRepository where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAllAsync();

    Task<T?> GetById(int id);

    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);

    Task<int> DeleteByIdAsync(int id);

    // we need it untile we implement unit of work
    //Task<int> CommitAsync();
}