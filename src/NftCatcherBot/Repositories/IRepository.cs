using System.Linq.Expressions;
using NftCatcherApi.Entities;

namespace NftCatcherApi.Repositories;

public interface IRepository<TEntity> where TEntity : Auditable
{
    ValueTask<TEntity> InsertAsync(TEntity entity);
        
    TEntity Update(TEntity entity);
    IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null, string[] includes = null);
    ValueTask<TEntity?> SelectAsync(Expression<Func<TEntity, bool>> expression, string[] includes = null);      
    ValueTask<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression);
    bool DeleteMany(Expression<Func<TEntity, bool>> expression);
        
    ValueTask<bool> SaveAsync();
}
