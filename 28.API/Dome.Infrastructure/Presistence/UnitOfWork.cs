using Dome.Core.Abstractions;
using Dome.Core.Entities;
using Dome.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Dome.Infrastructure.Presistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly IServiceProvider _serviceProvider;

    public UnitOfWork(AppDbContext context, IServiceProvider serviceProvider)
    {
        _context = context;
        _serviceProvider = serviceProvider;
    }

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public TRepo CustomRepository<TRepo>() where TRepo : IRepository
    {
        return _serviceProvider.GetRequiredService<TRepo>();
    }

    public async Task ExecuteInTransactionAsync(Func<Task> action)
    {
        await using IDbContextTransaction tx = await _context.Database.BeginTransactionAsync();

        try
        {
            await action();

            await tx.CommitAsync();
        }
        catch
        {
            await tx.RollbackAsync();
            throw;
        }
    }

    public IGenericRepository<T> Repository<T>() where T : BaseEntity
    {
        return _serviceProvider.GetRequiredService<IGenericRepository<T>>();
    }
}
