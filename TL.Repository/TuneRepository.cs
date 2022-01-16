using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;

namespace TL.Repository;

public interface ITuneRepository : IGenericRepository<Tune>
{
    Task AddAlternateTitlesAsync(int id, List<string> tunes);
    Task<IEnumerable<Tune>> GetAllTunes();
    Task Add(Tune tune);
    Task Delete(int id);
    Task<Tune> FindAsync(int id);
    Task Update(int id);
    Task<IEnumerable<Tune>> SortByTuneTypeAsync(TuneTypeEnum type);
    Task<IEnumerable<Tune>> SortByTuneKeyAsync(TuneKeyEnum key);
    Task<IEnumerable<Tune>> SortByTypeAndKeyAsync(TuneTypeEnum type, TuneKeyEnum key);
    Task<IEnumerable<Tune>> SortByExactComposerAsync(string composer);
    Task<IEnumerable<Tune>> GetByTitle(string title);


}

public class TuneRepository : GenericRepository<Tune>, ITuneRepository
{
    public TuneRepository(TuneLibraryContext context) : base(context)
    {
       
    }
    
    public async Task<IEnumerable<Tune>> GetAllTunes()
    {
        return await GetAll().ToListAsync();
    }

    public async Task Add(Tune tune)
    {
        await AddAsync(tune);
    }

    public async Task Delete(int id)
    {
        var tune = await FindAsync(id);
        await DeleteAsync(tune);
    }
    public override async Task<Tune> FindAsync(int id)
    {
        var tune = await Context.Tunes
            .Include(t => t.FeaturedOn)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (tune == null)
        {
            throw new Exception();
        }

        return tune;
    }
    
    public async Task Update(int id)
    {
        var tune = await FindAsync(id);
        await UpdateAsync(tune);
    }

    public async Task AddAlternateTitlesAsync(int id, List<string> tunes)
    {
        var tune = await Context.Tunes.FindAsync(id);

        if (tune == null)
        {
            throw new NullReferenceException();
        }
        tune.AddAlternateTitles(tunes);
        await Context.SaveChangesAsync();

    }

    public async Task<IEnumerable<Tune>> SortByTuneTypeAsync(TuneTypeEnum type)
    {
        return await GetByWhere(tune => tune.TuneType == type).ToListAsync();
    }

    public async Task<IEnumerable<Tune>> SortByTuneKeyAsync(TuneKeyEnum key)
    {
        return await GetByWhere(t => t.TuneKey == key).ToListAsync();
    }

    public async Task<IEnumerable<Tune>> SortByTypeAndKeyAsync(TuneTypeEnum type, TuneKeyEnum key)
    {
        return await GetByWhere(t => t.TuneType == type & t.TuneKey == key).ToListAsync();
    }

    public async Task<IEnumerable<Tune>> SortByExactComposerAsync(string composer)
    { 
        return await GetByWhere(t => t.Composer == composer).ToListAsync();
    }

    public async Task<IEnumerable<Tune>> GetByTitle(string title)
    {
        return await GetByWhere(t => t.Title == title).ToListAsync();
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