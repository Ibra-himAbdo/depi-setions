using Mapster;

namespace Application.Infrastructure;

internal class GenericRepository<T>(ApplicationDbContext dbContext)
    : IGenericRepository<T>
    where T : BaseEntity
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

    // Projection methods
    public async Task<IReadOnlyList<TResult>> ProjectAsync<TResult>(Expression<Func<T, TResult>> selector, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<T>()
            .AsNoTracking()
            .Select(selector)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TDto>> ProjectToAsync<TDto>(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = dbContext.Set<T>().AsNoTracking().AsQueryable();

        if (predicate is not null)
            query = query.Where(predicate);

        return await query
            .ProjectToType<TDto>()
            .ToListAsync(cancellationToken);
    }

    // Specification pattern methods
    public async Task<IReadOnlyList<T>> GetAllWithSpecificationAsync(ISpecifications<T> specifications, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specifications)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<T?> GetByIdWithSpecificationAsync(ISpecifications<T> specifications, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specifications)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<int> GetCountAsync(ISpecifications<T> specification, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification)
                    .AsNoTracking()
                    .CountAsync(cancellationToken: cancellationToken);
    }

    private IQueryable<T> ApplySpecification(ISpecifications<T> specifications)
        => SpecificationEvaluator<T>.GetQuery(dbContext.Set<T>(), specifications);
}