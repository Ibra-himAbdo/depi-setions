namespace Application.Infrastructure;

internal static class SpecificationEvaluator<T> where T : class, IEntity
{
    public static IQueryable<T> GetQueryFromSpecifications(IQueryable<T> inputQuery, ISpecifications<T> specifications)
    {
        IQueryable<T> query = inputQuery.AsQueryable();

        if (specifications.Criteria is not null)
            query = query.Where(specifications.Criteria);

        if (specifications.OrderBy is not null)
            query = query.OrderBy(specifications.OrderBy);
        else if (specifications.OrderByDescending is not null)
            query = query.OrderByDescending(specifications.OrderByDescending);

        if (specifications.IsPaginationEnable)
            query = query.Skip(specifications.Skip).Take(specifications.Take);

        if (specifications.Includes is not null)
            query = specifications.Includes.Aggregate(query, (currentQuery, includeQuery)
                => currentQuery.Include(includeQuery));

        // is the same code as the Aggregate at the obove code 
        //foreach (var includeQuery in specifications.Includes)
        //{
        //    query = query.Include(includeQuery);
        //}

        return query;
    }
}