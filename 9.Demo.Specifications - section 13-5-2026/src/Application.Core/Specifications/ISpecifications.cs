namespace Application.Core;

public interface ISpecifications<T> where T : class, IEntity
{
    Expression<Func<T,bool>>? Criteria { get; }

    List<Expression<Func<T,object>>>? Includes { get; }


    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }

    int Skip { get; }
    int Take { get; }

    bool IsPaginationEnable { get; }
}