namespace Demo.Core.Abstractions;

public interface ITodoItemRepository : IGenericRepository<TodoItem>
{
    Task<IReadOnlyList<TodoItem>> GetTodoItemsWithCategories();
    Task<TodoItem?> GetTodoItemWithCategory(int id);
}