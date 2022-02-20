using System.Linq.Expressions;
using EcommerceService.Core.Domain;
using EcommerceService.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EcommerceService.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Base
{
    private readonly DbSet<TEntity> _dbSet;
    private readonly EcommerceDbContext _context;

    public Repository(EcommerceDbContext context)
    {
        context.ChangeTracker.LazyLoadingEnabled = false;

        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public IQueryable<TEntity> Query()
    {
        return _dbSet.AsQueryable();
    }

    public async Task<ICollection<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public TEntity GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    // TODO I will add custom exception.
    public TEntity Find(Expression<Func<TEntity, bool>> match)
    {
        return _dbSet.SingleOrDefault(match);
    }

    public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
    {
        return await _dbSet.SingleOrDefaultAsync(match);
    }

    public ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match)
    {
        return _dbSet.Where(match).ToList();
    }

    public async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
    {
        return await _dbSet.Where(match).ToListAsync();
    }

    public TEntity Add(TEntity entity)
    {
        entity = setBaseToAdd(entity);
        _dbSet.Add(entity);
        _context.Entry(entity).State = EntityState.Added;

        return entity;
    }

    public TEntity Update(TEntity entity)
    {
        if (entity == null)
            return null;

        entity = setBaseToUpdate(entity);
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;

        return entity;
    }

    public void Delete(TEntity entity)
    {
        entity = setBaseToRemove(entity);
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Where(predicate);
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    private TEntity setBaseToAdd(TEntity entity)
    {
        entity.CreatedDate = DateTime.Now;
        return setBaseToUpdate(entity);
    }

    private TEntity setBaseToUpdate(TEntity entity)
    {
        entity.UpdatedDate = DateTime.Now;
        return entity;
    }

    private TEntity setBaseToRemove(TEntity entity)
    {
        entity.IsDeleted = true;
        return setBaseToUpdate(entity);
    }
}