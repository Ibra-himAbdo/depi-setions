namespace Application.Core;

public class BaseSpecifications<T> : ISpecifications<T> where T : class, IEntity
{
    public Expression<Func<T, bool>>? Criteria { get; protected set; }

    public List<Expression<Func<T, object?>>>? Includes { get; protected set; }

    public Expression<Func<T, object>>? OrderBy { get; protected set; }

    public Expression<Func<T, object>>? OrderByDescending { get; protected set; }

    public int Skip { get; protected set; }

    public int Take { get; protected set; }

    public bool IsPaginationEnable { get; protected set; }

    public BaseSpecifications()
    {
    }

    public BaseSpecifications(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    protected void AddOrderBy(Expression<Func<T, object>>? orderByExpression)
        => OrderBy = orderByExpression;

    protected void AddOrderByDescending(Expression<Func<T, object>>? orderByDescendingExpression)
        => OrderByDescending = orderByDescendingExpression;

    protected void ApplPagination(int skip, int take)
    {
        IsPaginationEnable = true;
        Skip = skip;
        Take = take;
    }
}