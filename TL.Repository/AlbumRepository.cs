using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TL.Data;
using TL.Domain;

namespace TL.Repository;

public interface IAlbumRepository : IGenericRepository<Album>
{
    Task AddAlbum(Album album);
    Task DeleteAlbum(int id);
    new Task<Album> FindAsync(int id);
    Task<IEnumerable<Album>> FindByArtist(Artist artist);
    Task<IEnumerable<Album>> GetAllAlbums();
    Task UpdateAlbum(int id, string title, int year);
}

public class AlbumRepository : GenericRepository<Album>, IAlbumRepository
{ 
    public AlbumRepository(TuneLibraryContext context) : base(context)
    {
        
    }

    public async Task AddAlbum(Album album)
    {
        await AddAsync(album);
    }

    public async Task DeleteAlbum(int id)
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
            throw new InvalidOperationException("Album not found");
        
        return album;
    }
    

    public async Task<IEnumerable<Album>> FindByArtist(Artist artist)
    {
        return await GetByWhere
            (x => x.Artists.Contains(artist)).ToListAsync();
    }

    public async Task<IEnumerable<Album>> GetAllAlbums()
    {
        return await GetEntities().ToListAsync();
    }
    
    public async Task UpdateAlbum(int id, string title, int year)
    {
        var album = await FindAsync(id);
        album.UpdateTitle(title);
        await SaveAsync();
    }

    public async Task UpdateYear(int id, int year)
    {
        var album = await FindAsync(id);
        album.UpdateYear(year);
        await SaveAsync();
    }
    
}