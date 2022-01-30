using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;

namespace TL.Repository;

public interface IAlbumRepository : IGenericRepository<Album>
{
    new Task<Album> FindAsync(int id);
    Task<bool> UpdateAlbum(Album album, string title, int year);
}

public class AlbumRepository : GenericRepository<Album>, IAlbumRepository
{ 
    public AlbumRepository(TuneLibraryContext context) : base(context)
    {
        
    }

    public override async Task<Album> FindAsync(int id)
    {
        var album = await Context.Albums
            .Include(x => x.Artists)
            .Include(x => x.TrackListing)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (album == null)
            throw new InvalidOperationException("Album not found");
        
        return album;
    }

    public async Task<bool> UpdateAlbum(Album album, string title, int year)
    {
        album.UpdateTitle(title);
        album.UpdateYear(year);
        return await SaveAsync() > 0;
    }
    
}