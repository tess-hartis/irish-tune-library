using LanguageExt;
using LanguageExt.SomeHelp;
using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;

namespace TL.Repository;

public interface ITuneRepository : IGenericRepository<Tune>
{
    new Task<Option<Tune>> FindAsync(int id);
}

public class TuneRepository : GenericRepository<Tune>, ITuneRepository
{
    public TuneRepository(TuneLibraryContext context) : base(context)
    {
       
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
}