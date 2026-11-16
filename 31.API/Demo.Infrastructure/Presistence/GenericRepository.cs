using Demo.Core.Abstractions;
using Demo.Core.Entities;
using Demo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Demo.Infrastructure.Presistence;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<TEntity> _table;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _table = _context.Set<TEntity>();
    }
    public async Task<IReadOnlyList<TEntity>> GetAllAsync()
    {
        return await _table.AsNoTracking().ToListAsync();
    }

    public async Task<TEntity?> GetById(int id)
    {
        // we need it untile we implement unit of wor
        return await _table.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        //return await _table.FindAsync(id);
    }

    public async Task<int> AddAsync(TEntity entity)
    {
        _table.Add(entity);

        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(TEntity entity)
    {
        _table.Update(entity);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(TEntity entity)
    {
        _table.Remove(entity);

        return await _context.SaveChangesAsync();
    }

    public async Task<int> DeleteByIdAsync(int id)
    {
        return await _table.Where(d => d.Id == id).ExecuteDeleteAsync();
    }

    // we need it untile we implement unit of work
    //public async Task<int> CommitAsync()
    //{
    //    return await _context.SaveChangesAsync();
    //}
}
