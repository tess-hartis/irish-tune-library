using LanguageExt;
using LanguageExt.SomeHelp;
using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;
using TL.Domain.ValueObjects.TuneValueObjects;

namespace TL.Repository;

public interface ITuneRepository : IGenericRepository<Tune>
{
    Task AddAlternateTitle(Tune tune, TuneTitle title);
    Task RemoveAlternateTitle(Tune tune, TuneTitle title);
    Task<Option<Tune>> FindAsync(int id);
}

public class TuneRepository : GenericRepository<Tune>, ITuneRepository
{
    public TuneRepository(TuneLibraryContext context) : base(context)
    {
       
    }
    

    public async Task AddAlternateTitle(Tune tune, TuneTitle title)
    {
        tune.AddAlternateTitle(title);
        await SaveAsync();
    }

    public async Task RemoveAlternateTitle(Tune tune, TuneTitle title)
    { 
        var toDelete = tune.AlternateTitles
            .First(x => x.Value == title.Value);
        tune.RemoveAlternateTitle(toDelete);
        await SaveAsync();
    }

    public override async Task<Option<Tune>> FindAsync(int id)
    {
        var result = await Context.Tunes
            .Include(x => x.AlternateTitles)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
            return Option<Tune>.None;

        return result.ToSome();
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