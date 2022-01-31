using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;
using TL.Domain.Exceptions;
using TL.Domain.Validators;

namespace TL.Repository;

public interface IAlbumRepository : IGenericRepository<Album>
{
    new Task<Album> FindAsync(int id);
    Task UpdateAlbum(Album album, string title, int year);
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
            throw new InvalidOperationException("Album not found");
        
        return album;
    }

    public async Task UpdateAlbum(Album album, string title, int year)
    {
        album.UpdateTitle(title);
        album.UpdateYear(year);
        
        var errors = new List<string>();
        var results = await _validator.ValidateAsync(album);
        if (results.IsValid == false)
        {
            foreach (var validationFailure in results.Errors)
            {
                errors.Add($"{validationFailure.ErrorMessage}");
            }
            
            throw new InvalidEntityException(string.Join(", ", errors));
        }
        
        await SaveAsync();
    }
    
}