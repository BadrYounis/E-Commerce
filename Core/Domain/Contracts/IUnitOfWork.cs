using Domain.Entities;

namespace Domain.Contracts;
public interface IUnitOfWork
{
    //SaveChanges()
    Task<int> SaveChangesAsync();
    //Method returns object from generic repository(entity)
    IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
}