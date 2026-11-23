using Dome.Core.Entities;

namespace Dome.Core.Abstractions;

public interface IUnitOfWork
{
    Task<int> CommitAsync();
    IGenericRepository<T> Repository<T>() where T : BaseEntity;
    TRepo CustomRepository<TRepo>() where TRepo : IRepository;
    Task ExecuteInTransactionAsync(Func<Task> action);
}