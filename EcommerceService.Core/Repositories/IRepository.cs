using System.Linq.Expressions;
using EcommerceService.Core.Domain;

namespace EcommerceService.Core.Repositories;

public interface IRepository<TEntity> where TEntity : Base
{
    IQueryable<TEntity> Query();

    Task<ICollection<TEntity>> GetAllAsync();

    TEntity GetById(int id);

    Task<TEntity> GetByIdAsync(int id);

    TEntity Find(Expression<Func<TEntity, bool>> match);

    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match);

    ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match);

    Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match);

    TEntity Add(TEntity entity);

    TEntity Update(TEntity entity);

    void Delete(TEntity entity);

    int SaveChanges();

    Task<int> SaveChangesAsync();
}