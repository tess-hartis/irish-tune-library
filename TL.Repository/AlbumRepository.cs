using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;
using TL.Domain.Exceptions;
using TL.Domain.ValueObjects.AlbumValueObjects;

namespace TL.Repository;

public interface IAlbumRepository : IGenericRepository<Album>
{
    new Task<Album> FindAsync(int id);
    Task<Album> UpdateAlbum(int id, AlbumTitle title, AlbumYear year);
    new Task DeleteAsync(int id);
    Task<IEnumerable<Album>> GetAll();
}

public class AlbumRepository : GenericRepository<Album>, IAlbumRepository
{ 
    public AlbumRepository(TuneLibraryContext context) : base(context)
    {
        
    }

    public override async Task<Album> FindAsync(int id)
    {
        var album = await Context.Albums.Include(a => a.Artists)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (album == null)
            throw new EntityNotFoundException($"Album with ID '{id}' was not found");
        
        return album;
    }
    
    public override async Task DeleteAsync(int id)
    {
        var album = await Context.Albums
            .Include(x => x.TrackListing)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (album == null)
            throw new EntityNotFoundException($"No album with ID of '{id}' was found");
        
        Context.Remove(album);
        await SaveAsync();
    }

    public async Task<Album> UpdateAlbum(int id, AlbumTitle title, AlbumYear year)
    {
        var album = await FindAsync(id);
        album.Update(title, year);
        await SaveAsync();
        return album;
    }

    public async Task<IEnumerable<Album>> GetAll()
    {
        return await Context.Albums.Include(a => a.Artists).ToListAsync();
    }


}