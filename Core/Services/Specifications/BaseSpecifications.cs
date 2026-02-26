using Domain.Contracts;
using Domain.Entities;
using System.Linq.Expressions;

namespace Services.Specifications;
internal abstract class BaseSpecifications<TEntity, TKey>
    : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    protected BaseSpecifications(Expression<Func<TEntity, bool>>? criteria)
    {
        Criteria = criteria;
    }
    public Expression<Func<TEntity, bool>>? Criteria { get; private set; }
    public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];
    //.AddIncludes(p => p.ProductBrand)
    //.AddIncludes(p => p.ProductType)
    protected void AddIncludes(Expression<Func<TEntity, object>> includeExpression)
    {
        IncludeExpressions.Add(includeExpression);   //Take each include exists inside list and add it
    }
}