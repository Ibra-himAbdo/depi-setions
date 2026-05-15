namespace Application.Core;

public interface IUnitOfWork : IAsyncDisposable
{
    IGenericRepository<T> Repository<T>() where T : class, IEntity;
    IProductRepository ProductRepository { get; }

    Task<int> CompleteAsync(CancellationToken cancellationToken = default);
}