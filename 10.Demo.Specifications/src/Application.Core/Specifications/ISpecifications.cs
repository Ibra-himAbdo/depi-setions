namespace Application.Core;

public interface ISpecifications<T> where T : BaseEntity
{
    public Expression<Func<T, bool>>? Criteria { get; }
    public List<Expression<Func<T, object>>> Includes { get; }
    //List<Func<IQueryable<T>, IQueryable<T>>> Includes { get; } // for adding support for ThenInclude (query chaining)

    public Expression<Func<T, object>>? OrderBy { get; }
    public Expression<Func<T, object>>? OrderByDescending { get; }

    public int Skip { get; }
    public int Take { get; }
    public bool IsPaginationEnabled { get; }
}