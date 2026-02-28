using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Contracts;
public interface ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    //Signature for property [Expression => Where]
    public Expression<Func<TEntity, bool>>? Criteria { get; }
    //Signature for property [Expression => Include]
    public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
    //OrderBy - OrderByDescending [Expression]
    public Expression<Func<TEntity, object>> OrderBy { get;}
    public Expression<Func<TEntity, object>> OrderByDescending { get; }
    //Pagination [Skip - Take]
    public int Skip { get;}
    public int Take { get; }
    public bool IsPaginated { get;}
}