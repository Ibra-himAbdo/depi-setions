using Dome.Core.Entities;

namespace Dome.Core.Abstractions;

public interface IGenericRepository<T> : IRepository where T : BaseEntity
{
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
}
