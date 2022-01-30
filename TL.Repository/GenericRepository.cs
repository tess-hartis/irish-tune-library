using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TL.Common;
using TL.Data;

namespace TL.Repository;

public interface IGenericRepository<T> where T : class
{
    IQueryable<T> GetEntities();
    IQueryable<T> GetByWhere(Expression<Func<T, bool>> predicate);
    Task<bool> AddAsync(T entity);
    Task<bool> DeleteAsync(int id);
    Task<T> FindAsync(int id);
    Task<int> SaveAsync();
}

public abstract class GenericRepository<T>
    : IGenericRepository<T> where T : class
{
    protected TuneLibraryContext Context;

    public GenericRepository(TuneLibraryContext context)
    {
        Context = context;
    }

    public IQueryable<T> GetEntities()
    {
        return Context.Set<T>().AsNoTracking();
    }

    public IQueryable<T> GetByWhere(Expression<Func<T, bool>> predicate)
    {
        var result = Context.Set<T>().Where(predicate).AsNoTracking();

        return result;
    }

    public virtual async Task<bool> AddAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
        return await Context.SaveChangesAsync() > 0;
    }

    public virtual async Task<bool> DeleteAsync(int id)
    {
        var entity = await Context.Set<T>().FindAsync(id);
        if (entity == null)
        {
            return false;
        }
        Context.Set<T>().Remove(entity);
        return await Context.SaveChangesAsync() > 0;
    }

    public virtual async Task<T> FindAsync(int id)
    {
        var result = await Context.Set<T>().FindAsync(id);
        if (result == null)
            throw new NullReferenceException($"No entity with ID '{id}' was found");
        return result;
    }

    public virtual async Task<int> SaveAsync()
    {
        return await Context.SaveChangesAsync();
    }
}