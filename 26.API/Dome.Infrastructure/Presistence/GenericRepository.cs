using Dome.Core.Abstractions;
using Dome.Core.Entities;
using Dome.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Dome.Infrastructure.Presistence;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _table;

    public GenericRepository(AppDbContext context)
    {
        _context = context;

        _table = _context.Set<T>();
    }

    public async Task<List<T>> GetAllAsync()
    {
        //if(typeof(T)== typeof(Category))
        //{
        //    return await _context.Categories.ToListAsync() as List<T> ?? new();
        //}
        //else if(typeof(T) == typeof(TodoItem))
        //{
        //    return await _context.TodoItems.ToListAsync() as List<T> ?? new();
        //}

        return await _table.AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _table.FindAsync(id);
    }

    public async Task<int> AddAsync(T entity)
    {
        _table.Add(entity);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(T entity)
    {
        _table.Update(entity);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(T entity)
    {
        _table.Remove(entity);
        return await _context.SaveChangesAsync();
    }
}