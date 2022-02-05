using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;
using TL.Domain.Validators;
using TL.Domain.ValueObjects.TuneValueObjects;

namespace TL.Repository;

public interface ITuneRepository : IGenericRepository<Tune>
{
    Task UpdateTune(int id, TuneTitle title, TuneComposer composer, TuneTypeEnum type, TuneKeyEnum key);
    Task AddAlternateTitle(int id, TuneTitle title);
    Task RemoveAlternateTitle(int id, TuneTitle title);
}

public class TuneRepository : GenericRepository<Tune>, ITuneRepository
{
    public TuneRepository(TuneLibraryContext context) : base(context)
    {
       
    }

    public async Task UpdateTune(int id, TuneTitle title, TuneComposer composer, TuneTypeEnum type, TuneKeyEnum key)
    {
        var tune = await FindAsync(id);
        tune.Update(title, composer, type, key);
        await SaveAsync();
    }

    public async Task AddAlternateTitle(int id, TuneTitle title)
    {
        var tune = await FindAsync(id);
        tune.AddAlternateTitle(title);
        await SaveAsync();
    }

    public async Task RemoveAlternateTitle(int id, TuneTitle title)
    {
        var tune = await FindAsync(id);
        tune.RemoveAlternateTitle(title);
        await SaveAsync();
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