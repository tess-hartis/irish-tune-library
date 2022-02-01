using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;
using TL.Domain.Exceptions;
using TL.Domain.Validators;

namespace TL.Repository;

public interface IAlbumRepository : IGenericRepository<Album>
{
    new Task<Album> FindAsync(int id);
    Task UpdateAlbum(int id, string title, int year);
}

public class AlbumRepository : GenericRepository<Album>, IAlbumRepository
{ 
    public AlbumRepository(TuneLibraryContext context) : base(context)
    {
        
    }

    private readonly AlbumValidator _validator = new AlbumValidator();

    public override async Task<Album> FindAsync(int id)
    {
        var album = await Context.Albums
            .Include(x => x.Artists)
            .Include(x => x.TrackListing)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (album == null)
            throw new EntityNotFoundException($"Album with ID '{id}' was not found");
        
        return album;
    }

    public async Task UpdateAlbum(int id, string title, int year)
    {
        var album = await FindAsync(id);
        album.Update(title, year);
        await SaveAsync();
    }
    
}