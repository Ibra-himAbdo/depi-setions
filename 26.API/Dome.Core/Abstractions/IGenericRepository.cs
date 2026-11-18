using Dome.Core.Entities;

namespace Dome.Core.Abstractions;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<int> AddAsync(T entity);
    Task<int> DeleteAsync(T entity);
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<int> UpdateAsync(T entity);
}
