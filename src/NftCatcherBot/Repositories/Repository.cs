using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NftCatcherApi.DbContexts;
using NftCatcherApi.Entities;

namespace NftCatcherApi.Repositories;

public class Repository<TEntity>(BotDbContext botDbContext) : IRepository<TEntity>
    where TEntity : Auditable
{
    private readonly DbSet<TEntity> _dbSet = botDbContext.Set<TEntity>();

    /// <summary>
    /// Deletes first item that matched expression and keep track of it until change saved
    /// </summary>
    /// <param name="expression"></param>
    /// <returns>true if action is successful, false if unable to delete</returns>
    public async ValueTask<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await this.SelectAsync(expression);

        if (entity is not null)
        {
            entity.IsDeleted = true;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Deletes all elements if expression matches
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public bool DeleteMany(Expression<Func<TEntity, bool>> expression)
    {
        var entities = _dbSet.Where(expression);
        if (entities.Any())
        {
            foreach (var entity in entities)
                entity.IsDeleted = true;

            return true;
        }

        return false;
    }

    /// <summary>
    /// Inserts element to a table and keep track of it until change saved
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async ValueTask<TEntity> InsertAsync(TEntity entity)
    {
        EntityEntry<TEntity> entry = await this._dbSet.AddAsync(entity);

        return entry.Entity;
    }

    public async ValueTask<bool> InsertAsync(IEnumerable<TEntity> entity)
    {
        await this._dbSet.AddRangeAsync(entity);
        return true;
    }

    /// <summary>
    /// Saves tracking changes and write them to database permenantly
    /// </summary>
    /// <returns></returns>
    public async ValueTask<bool> SaveAsync()
    {
        return await botDbContext.SaveChangesAsync() >= 0;
    }

    /// <summary>
    /// Selects all elements from table that matches condition and include relations
    /// </summary>
    /// <returns></returns>
    public IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null, string[] includes = null)
    {
        IQueryable<TEntity> query = expression is null ? this._dbSet : this._dbSet.Where(expression);

        if (includes is not null)
        {
            foreach (string include in includes)
            {
                query = query.Include(include);
            }
        }

        return query;
    }

    /// <summary>
    /// selects element from a table specified with expression and can includes relations
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public async ValueTask<TEntity?> SelectAsync(Expression<Func<TEntity, bool>> expression, string[] includes = null)
        => await this.SelectAll(expression, includes).FirstOrDefaultAsync(t => !t.IsDeleted);

    /// <summary>
    /// Updates entity and keep track of it until change saved
    /// </summary>
    /// <param name="id"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    public TEntity Update(TEntity entity)
    {
        EntityEntry<TEntity> entryentity = botDbContext.Update(entity);

        return entryentity.Entity;
    }
}