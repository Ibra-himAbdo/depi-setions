using Mapster;

namespace Application.Infrastructure;

internal class GenericRepository<T>(ApplicationDbContext dbContext)
    : IGenericRepository<T>
    where T : class, IEntity
{
    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<T>()
                .AsNoTracking()
                .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<T?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<T>()
            .FindAsync([id], cancellationToken: cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await dbContext.Set<T>()
                .AddAsync(entity, cancellationToken: cancellationToken);
    }

    public Task UpdateAsync(T entity)
    {
        // Only mark as modified if not already tracked
        if (dbContext.Entry(entity)
                .State == EntityState.Detached)
            dbContext.Set<T>()
                .Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity)
    {
        if (dbContext.Entry(entity)
                .State == EntityState.Detached)
            dbContext.Set<T>()
                .Attach(entity);

        dbContext.Set<T>()
            .Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<int> GetCountAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<T>()
            .AsNoTracking()
            .CountAsync(predicate, cancellationToken: cancellationToken);
    }

    public async Task<bool> ExistsAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<T>()
                .AsNoTracking()
                .AnyAsync(predicate, cancellationToken: cancellationToken);
    }

    public async Task<TResult> GetMaxAsync<TResult>(Expression<Func<T, TResult>> selector, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<T>().AsNoTracking().MaxAsync(selector, cancellationToken: cancellationToken);
    }

    public async Task<TResult> GetMinAsync<TResult>(Expression<Func<T, TResult>> selector, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<T>().AsNoTracking().MinAsync(selector, cancellationToken: cancellationToken);
    }

    public async Task<T?> FindAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(predicate, cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyList<T>> FindAllAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<T>()
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyList<T>> GetAllWithSpecificationsAsync(ISpecifications<T> specifications, CancellationToken cancellationToken = default)
    {
        return await ApplySpecifications(specifications).AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByIdWithSpecificationsAsync(ISpecifications<T> specifications, CancellationToken cancellationToken = default)
    {
        return await ApplySpecifications(specifications).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<int> GetCountAsync(ISpecifications<T> specifications, CancellationToken cancellationToken = default)
    {
        return await ApplySpecifications(specifications).AsNoTracking().CountAsync(cancellationToken);
    }

    private IQueryable<T> ApplySpecifications(ISpecifications<T> specifications)
        => SpecificationEvaluator<T>.GetQueryFromSpecifications(dbContext.Set<T>(), specifications);

    // projection
    public async Task<IReadOnlyList<TResult?>> ProjectAsync<TResult>(Expression<Func<T, TResult?>> selector, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<T>()
            .AsNoTracking()
            .Select(selector)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TResult?>> ProjectToAsync<TResult>(CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<T>()
           .AsNoTracking()
           .ProjectToType<TResult>()
           .ToListAsync(cancellationToken);
    }
}