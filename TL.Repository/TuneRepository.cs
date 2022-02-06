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
    Task<Tune> FindAsync(int id);
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
        var toDelete = tune.AlternateTitles
            .First(x => x.Value == title.Value);
        tune.RemoveAlternateTitle(toDelete);
        await SaveAsync();
    }

    public override async Task<Tune> FindAsync(int id)
    {
        var result = await Context.Tunes
            .Include(x => x.AlternateTitles)
            .FirstOrDefaultAsync(x => x.Id == id);

        return result;
    }

    public async Task<IEnumerable<TuneTitle>> GetAltTitles(int id)
    {
        var tune = await Context.Tunes.FindAsync(id);
        return tune.AlternateTitles.ToList();
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