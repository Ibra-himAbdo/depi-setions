using Dome.Core.Entities;

namespace Dome.Core.Abstractions;

public interface ICategoryRepository
{
    Task<int> AddAsync(Category category);
    Task<int> DeleteAsync(Category category);
    Task<List<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task<int> UpdateAsync(Category category);
}
