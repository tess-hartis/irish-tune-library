using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;
using TL.Domain.Validators;

namespace TL.Repository;

public interface ITuneRepository : IGenericRepository<Tune>
{
    Task UpdateTune(int id, string title, string composer, TuneTypeEnum type, TuneKeyEnum key);
    Task AddAlternateTitle(int id, string title);
    Task RemoveAlternateTitle(int id, string title);
}

public class TuneRepository : GenericRepository<Tune>, ITuneRepository
{
    public TuneRepository(TuneLibraryContext context) : base(context)
    {
       
    }

    private readonly TuneValidator _validator = new TuneValidator();
    
    public async Task UpdateTune(int id, string title, string composer, TuneTypeEnum type, TuneKeyEnum key)
    {
        var tune = await FindAsync(id);
        tune.Update(title, composer, type, key);
        await SaveAsync();
    }

    public async Task AddAlternateTitle(int id, string title)
    {
        var tune = await FindAsync(id);
        tune.AddAlternateTitle(title);
        await SaveAsync();
    }

    public async Task RemoveAlternateTitle(int id, string title)
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