using Demo.Core.Abstractions;
using Demo.Core.Entities;
using Demo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Demo.Infrastructure.Presistence;

public class TodoItemRepository : GenericRepository<TodoItem>, ITodoItemRepository
{
    public TodoItemRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<TodoItem>> GetTodoItemsWithCategories()
    {
        // eagr loading
        return await _table.AsNoTracking().Include(i => i.Category).ToListAsync();
        //return await _table.AsNoTracking().Join()
    }

    public async Task<TodoItem?> GetTodoItemWithCategory(int id)
    {
        return await _table.AsNoTracking().Include(i => i.Category).FirstOrDefaultAsync(i => i.Id == id);
    }
}