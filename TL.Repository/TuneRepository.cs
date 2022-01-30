using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;

namespace TL.Repository;

public interface ITuneRepository : IGenericRepository<Tune>
{
    Task<bool> UpdateTune(int id, string title, string composer, TuneTypeEnum type, TuneKeyEnum key);
    Task<bool> AddAlternateTitle(int id, string title);
    Task<bool> RemoveAlternateTitle(int id, string title);
}

public class TuneRepository : GenericRepository<Tune>, ITuneRepository
{
    public TuneRepository(TuneLibraryContext context) : base(context)
    {
       
    }

    public async Task<bool> UpdateTune(int id, string title, string composer, TuneTypeEnum type, TuneKeyEnum key)
    {
        var tune = await FindAsync(id);
        tune.UpdateTitle(title);
        tune.UpdateComposer(composer);
        tune.UpdateType(type);
        tune.UpdateKey(key);
        return await SaveAsync() > 0;
    }

    public async Task<bool> AddAlternateTitle(int id, string title)
    {
        var tune = await FindAsync(id);
        tune.AddAlternateTitle(title);
        return await SaveAsync() > 0;
    }

    public async Task<bool> RemoveAlternateTitle(int id, string title)
    {
        var tune = await FindAsync(id);
        tune.RemoveAlternateTitle(title);
        return await SaveAsync() > 0;
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