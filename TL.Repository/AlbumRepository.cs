using LanguageExt;
using LanguageExt.SomeHelp;
using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;

namespace TL.Repository;

public interface IAlbumRepository : IGenericRepository<Album>
{
    new Task<Option<Album>> FindAsync(int id);
    Task<IEnumerable<Album>> GetAll();
}

public class AlbumRepository : GenericRepository<Album>, IAlbumRepository
{ 
    public AlbumRepository(TuneLibraryContext context) : base(context)
    {
        
    }

    public override async Task<Option<Album>> FindAsync(int id)
    {
        var album = await Context.Albums
            .Include(a => a.Artists)
            .Include(a => a.TrackListing)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (album == null)
            return Option<Album>.None;

        return album.ToSome();
    }
    
    public async Task<IEnumerable<Album>> GetAll()
    {
        return await Context.Albums
            .Include(a => a.Artists)
            .Include(a => a.TrackListing)
            .ToListAsync();
    }
}