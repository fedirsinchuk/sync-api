using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Sync.DAL.Repositories;

public interface IGenericRepository<T>
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task RemoveAsync(T entity);
}

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly SyncDataBaseContext _context;
    private DbSet<T> _dbSet;

    public GenericRepository(SyncDataBaseContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        return entity;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        var entities = await _dbSet.ToListAsync();
        return entities;
    }

    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
    {
        var entity = await _dbSet.Where(expression).ToListAsync();
        return entity;
    }

    public virtual async Task AddAsync(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
    
    public virtual async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public virtual async Task RemoveAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
}