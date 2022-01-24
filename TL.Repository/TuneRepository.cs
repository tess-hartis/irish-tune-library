using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;

namespace TL.Repository;

public interface ITuneRepository : IGenericRepository<Tune>
{
    Task<IEnumerable<Tune>> GetAllTunes();
    Task AddTune(Tune tune);
    Task DeleteTune(int id);
    new Task<Tune> FindAsync(int id);
    Task<IEnumerable<Tune>> FindByType(TuneTypeEnum type);
    Task<IEnumerable<Tune>> FindByKey(TuneKeyEnum key);
    Task<IEnumerable<Tune>> FindByTypeAndKey(TuneTypeEnum type, TuneKeyEnum key);
    Task<IEnumerable<Tune>> FindByExactComposer(string composer);
    Task<IEnumerable<Tune>> FindByExactTitle(string title);
    Task UpdateTune(int id, string title, string composer, TuneTypeEnum type, TuneKeyEnum key);
    Task AddAlternateTitle(int id, string title);
    Task RemoveAlternateTitle(int id, string title);

}

public class TuneRepository : GenericRepository<Tune>, ITuneRepository
{
    public TuneRepository(TuneLibraryContext context) : base(context)
    {
       
    }
    
    public async Task<IEnumerable<Tune>> GetAllTunes()
    {
        return await GetEntities().ToListAsync();
    }

    public async Task AddTune(Tune tune)
    {
        await AddAsync(tune);
    }

    public async Task DeleteTune(int id)
    {
        var tune = await FindAsync(id);
        await DeleteAsync(tune);
    }
    
    public override async Task<Tune> FindAsync(int id)
    {
        var tune = await Context.Tunes
            .FirstOrDefaultAsync(t => t.Id == id);

        if (tune == null)
            throw new InvalidOperationException("Tune not found");

        return tune;
    }

    public async Task UpdateTune(int id, string title, string composer, TuneTypeEnum type, TuneKeyEnum key)
    {
        var tune = await FindAsync(id);
        tune.UpdateTitle(title);
        tune.UpdateComposer(composer);
        tune.UpdateType(type);
        tune.UpdateKey(key);
        await SaveAsync();
    }

    public async Task AddAlternateTitle(int id, string title)
    {
        var tune = await FindAsync(id);
        tune.AddAlternateTitle(title);
        await Context.SaveChangesAsync();

    }
    
    public async Task RemoveAlternateTitle(int id, string title)
    {
        var tune = await FindAsync(id);
        tune.RemoveAlternateTitle(title);
        await Context.SaveChangesAsync();

    }

    public async Task<IEnumerable<Tune>> FindByType(TuneTypeEnum type)
    {
        return await GetByWhere(tune => tune.TuneType == type).ToListAsync();
    }

    public async Task<IEnumerable<Tune>> FindByKey(TuneKeyEnum key)
    {
        return await GetByWhere(t => t.TuneKey == key).ToListAsync();
    }

    public async Task<IEnumerable<Tune>> FindByTypeAndKey(TuneTypeEnum type, TuneKeyEnum key)
    {
        return await GetByWhere(t => t.TuneType == type & t.TuneKey == key).ToListAsync();
    }

    public async Task<IEnumerable<Tune>> FindByExactComposer(string composer)
    { 
        return await GetByWhere(t => t.Composer == composer).ToListAsync();
    }

    public async Task<IEnumerable<Tune>> FindByExactTitle(string title)
    {
        return await GetByWhere(t => t.Title == title).ToListAsync();
    }

    public async Task<int> SaveChanges()
    {
        return await Context.SaveChangesAsync();
    }

    // public async Task<List<Tune>> AnthonyExampleRegex(string query)
    // {
    //     var regex = new Regex("???? + query + ???");
    //     
    //     var list = await _context.Tunes
    //         .Where(x => regex.IsMatch(x.Title)).ToListAsync();
    //     
    //     return list;
    // }
}