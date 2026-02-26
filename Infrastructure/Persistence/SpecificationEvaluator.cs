using Domain.Contracts;
using Domain.Entities;

namespace Persistence;
internal static class SpecificationEvaluator
{
    public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> inputQuery,
        ISpecifications<TEntity, TKey> specifications) where TEntity : BaseEntity<TKey>
    {
        var query = inputQuery;

        if (specifications.Criteria is not null)   //Where
            query = query.Where(specifications.Criteria);

        if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Count > 0)  //Includes
        {
            //foreach (var expression in specifications.IncludeExpressions)
            //    query = query.Include(expression);

            query = specifications.IncludeExpressions.Aggregate(query, (currentQuery, expression) => currentQuery.Include(expression));
        }

        return query;
    }
}