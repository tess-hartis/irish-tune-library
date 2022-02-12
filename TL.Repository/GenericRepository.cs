using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain.Exceptions;

namespace TL.Repository;

public interface IGenericRepository<T> where T : class
{
    IQueryable<T> GetEntities();
    IQueryable<T> GetByWhere(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    Task DeleteAsync(int id);
    Task<T> FindAsync(int id);
    Task<T> UpdateAsync(int id);
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

    public virtual async Task AddAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
        await Context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(int id)
    {
        var entity = await Context.Set<T>().FindAsync(id);
        if (entity == null)
            throw new EntityNotFoundException($"No entity with ID '{id}' was found");
        
        Context.Set<T>().Remove(entity);
        await Context.SaveChangesAsync();
    }

    public virtual async Task<T> FindAsync(int id)
    {
        var result = await Context.Set<T>().FindAsync(id);
        if (result == null)
            throw new EntityNotFoundException($"No entity with ID '{id}' was found");
        return result;
    }
    
    public virtual async Task<T> UpdateAsync(int id)
    {
        var entity = await Context.Set<T>().FindAsync(id);
        Context.Set<T>().Update(entity);
        await Context.SaveChangesAsync();
        return entity;

    }

    public virtual async Task<int> SaveAsync()
    {
        return await Context.SaveChangesAsync();
    }
}