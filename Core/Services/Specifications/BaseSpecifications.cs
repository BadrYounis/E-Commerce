using Domain.Contracts;
using Domain.Entities;
using System.Linq.Expressions;

namespace Services.Specifications;
internal abstract class BaseSpecifications<TEntity, TKey>
    : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    #region Criteria (Where)
    protected BaseSpecifications(Expression<Func<TEntity, bool>>? criteria)
    {
        Criteria = criteria;
    }
    public Expression<Func<TEntity, bool>>? Criteria { get; private set; }
    #endregion

    #region Includes
    public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

    //.AddIncludes(p => p.ProductBrand)
    //.AddIncludes(p => p.ProductType)
    protected void AddIncludes(Expression<Func<TEntity, object>> includeExpression)
    {
        IncludeExpressions.Add(includeExpression);   //Take each include exists inside list and add it
    }
    #endregion

    #region Sorting (OrderBy - OrderByDescending) 
    public Expression<Func<TEntity, object>> OrderBy { get; private set; }
    public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }
    protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        => OrderBy = orderByExpression;
    protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
        => OrderByDescending = orderByDescendingExpression;
    #endregion

    #region Pagination
    public int Skip { get; private set; }
    public int Take { get; private set; }
    public bool IsPaginated { get; private set; }
    protected void ApplyPagination(int pageSize, int pageIndex)
    {
        IsPaginated = true;
        Take = pageSize;
        Skip = (pageIndex - 1) * pageSize;
    }
    #endregion
}