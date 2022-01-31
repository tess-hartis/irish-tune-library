using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;
using TL.Domain.Exceptions;
using TL.Domain.Validators;

namespace TL.Repository;

public interface IArtistRepository : IGenericRepository<Artist>
{
    Task UpdateArtist(Artist artist, string name);
}

public class ArtistRepository : GenericRepository<Artist>, IArtistRepository
{
    public ArtistRepository(TuneLibraryContext context) : base(context)
    {
       
    }

    private readonly ArtistValidator _validator = new ArtistValidator();

    public async Task UpdateArtist(Artist artist, string name)
    {
        artist.UpdateName(name);
        
        var errors = new List<string>();
        var results = await _validator.ValidateAsync(artist);
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