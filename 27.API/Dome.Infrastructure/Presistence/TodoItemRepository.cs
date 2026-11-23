using Dome.Core.Abstractions;
using Dome.Core.Entities;
using Dome.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dome.Infrastructure.Presistence;

public class TodoItemRepository : GenericRepository<TodoItem>, ITodoItemRepository
{
    public TodoItemRepository(AppDbContext context) : base(context)
    {
    }

    public Task<List<TodoItem>> GetAllTodoItemsWithCategories()
    {
        return _table.AsNoTracking().Include(i => i.Category).ToListAsync();

        //var data =  _table.Join(_context.Categories, 
        //    i => i.CategoryId,
        //    c => c.Id,
        //    (todoItem,category) => {
        //        todoItem.Category = category;

        //        return todoItem;
        //});
    }

    public async Task<TodoItem?> GetTodoItemByIdWithCategory(int id)
    {
        return await _table.Include(i => i.Category).FirstOrDefaultAsync(i => i.Id == id);
    }
}
