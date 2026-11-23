namespace Demo.Core.Abstractions;

public interface IUnitOfWork
{
    IGenericRepository<T> Repository<T>() where T : BaseEntity;

    TRepo CustomRepository<TRepo>() where TRepo : IRepository;

    Task<int> CommitAsync();

    Task ExecuteInTransactionAsync(Func<Task> action);
}
