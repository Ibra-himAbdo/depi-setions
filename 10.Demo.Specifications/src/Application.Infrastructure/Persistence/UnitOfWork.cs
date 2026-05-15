namespace Application.Infrastructure;

internal class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    private readonly Hashtable _repositories = [];

    public IGenericRepository<T> Repository<T>() where T : BaseEntity
        => GetOrCreateRepository<IGenericRepository<T>, GenericRepository<T>>();

    public IProductRepository ProductRepository 
        => GetOrCreateRepository<IProductRepository, ProductRepository>();

    private TRepo GetOrCreateRepository<TRepo, TConcreteRepo>()
        where TConcreteRepo : TRepo
    {
        var key = typeof(TRepo).FullName;

        if (_repositories.ContainsKey(key!))
            return (TRepo)_repositories[key!]!;

        var repository =
            (TRepo)Activator.CreateInstance(typeof(TConcreteRepo), context)!;

        _repositories.Add(key!, repository);

        return (TRepo)_repositories[key!]!;
    }

    public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
        => await context.SaveChangesAsync(cancellationToken: cancellationToken);

    public async ValueTask DisposeAsync()
        => await context.DisposeAsync();
}