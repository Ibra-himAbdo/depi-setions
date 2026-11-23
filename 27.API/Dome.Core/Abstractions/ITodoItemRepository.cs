using Dome.Core.Entities;

namespace Dome.Core.Abstractions;

public interface ITodoItemRepository : IGenericRepository<TodoItem>
{
    Task<List<TodoItem>> GetAllTodoItemsWithCategories();
    Task<TodoItem?> GetTodoItemByIdWithCategory(int id);
}
