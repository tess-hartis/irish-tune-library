using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TL.Common;
using TL.Data;

namespace TL.Repository;

public interface IGenericRepository<T> where T : class
{
    IQueryable<T> GetEntities();
    IQueryable<T> GetByWhere(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<T> FindAsync(int id);
    Task SaveAsync();
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
        // if (!result.IsAny())
        // {
        //     throw new Exception();
        // }

        return result;
    }

    public virtual async Task AddAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
        await Context.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(T entity)
    {
        Context.Update(entity);
        await Context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(T entity)
    {
        Context.Set<T>().Remove(entity);
        await Context.SaveChangesAsync();
    }

    public virtual async Task<T> FindAsync(int id)
    {
        var result = await Context.Set<T>().FindAsync(id);
        if (result == null)
        {
            throw new NullReferenceException();
        }

        return result;
    }

    public virtual async Task SaveAsync()
    {
        await Context.SaveChangesAsync();
    }
}