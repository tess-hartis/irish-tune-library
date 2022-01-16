using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;

namespace TL.Repository;

public interface IAlbumRepository : IGenericRepository<Album>
{
    Task Add(Album album);
    Task Delete(int id);
    new Task<Album> FindAsync(int id);
    Task<Album?> FindByTrackFeatured(Track track);
    Task Update(int id);
    Task<IEnumerable<Album>> GetAllAlbums();
    Task<IEnumerable<Album>> GetByTitle(string title);
}

public class AlbumRepository : GenericRepository<Album>, IAlbumRepository
{ 
    public AlbumRepository(TuneLibraryContext context) : base(context)
    {
        
    }

    public async Task Add(Album album)
    {
        await AddAsync(album);
    }

    public async Task Delete(int id)
    {
        var album = await FindAsync(id);
        await DeleteAsync(album);
        
    }

    public override async Task<Album> FindAsync(int id)
    {
        var album = await Context.Albums
            .Include(a => a.TrackListing
                .OrderBy(t => t.TrackNumber))
            .ThenInclude(t => t.TuneList)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (album == null)
        {
            throw new Exception();
        }

        return album;
    }

    public async Task<Album?> FindByTrackFeatured(Track track)
    {
        return await Context.Albums
            .FirstOrDefaultAsync(a => a.TrackListing.Contains(track));
    }

    public async Task Update(int id)
    {
        var album = await FindAsync(id);
        await UpdateAsync(album);
    }

    public async Task<IEnumerable<Album>> GetAllAlbums()
    {
        return await GetAll().ToListAsync();
    }

    public async Task<IEnumerable<Album>> GetByTitle(string title)
    {
        return await GetByWhere(album => album.Title == title).ToListAsync();
    }
    
    

}