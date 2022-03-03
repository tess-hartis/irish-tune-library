using LanguageExt;
using LanguageExt.SomeHelp;
using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;

namespace TL.Repository;

public interface IArtistRepository : IGenericRepository<Artist>
{
    Task<IEnumerable<Artist>> GetAllArtists();
    new Task<Option<Artist>> FindAsync(int id);
}

public class ArtistRepository : GenericRepository<Artist>, IArtistRepository
{
    public ArtistRepository(TuneLibraryContext context) : base(context)
    {
       
    }

    public async Task<IEnumerable<Artist>> GetAllArtists()
    {
        return await Context.Artists.Include(a => a.Albums).ToListAsync();
    }

    public override async Task<Option<Artist>> FindAsync(int id)
    {
        var result = await Context.Artists.FirstOrDefaultAsync(x => x.Id == id);
        if (result == null)
            return Option<Artist>.None;

        return result.ToSome();
    }
}