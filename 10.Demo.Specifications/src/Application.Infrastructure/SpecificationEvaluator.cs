namespace Application.Infrastructure;

public static class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecifications<T> specifications)
    {
        IQueryable<T> query = inputQuery; //.AsSplitQuery();
        if (specifications.Criteria is not null)
            query = query.Where(specifications.Criteria);

        if (specifications.OrderBy is not null)
            query = query.OrderBy(specifications.OrderBy);
        else if (specifications.OrderByDescending is not null)
            query = query.OrderByDescending(specifications.OrderByDescending);

        if (specifications.IsPaginationEnabled)
            query = query
                .Skip(specifications.Skip)
                .Take(specifications.Take);

        query = specifications.Includes.Aggregate(query, (currentQuery, includeQuery)
            => currentQuery.Include(includeQuery));

        // this lines of code is the same as the above code, but it is more readable and easier to understand 
        //var resultQuery = query;

        //foreach (var includeQuery in specifications.Includes)
        //{
        //    resultQuery = resultQuery.Include(includeQuery);
        //}

        return query;
    }
}