using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;
using TL.Domain.Exceptions;
using TL.Domain.Validators;

namespace TL.Repository;

public interface IArtistRepository : IGenericRepository<Artist>
{
    Task UpdateArtist(int id, string name);
}

public class ArtistRepository : GenericRepository<Artist>, IArtistRepository
{
    public ArtistRepository(TuneLibraryContext context) : base(context)
    {
       
    }

    private readonly ArtistValidator _validator = new ArtistValidator();

    public async Task UpdateArtist(int id, string name)
    {
        var artist = await FindAsync(id);
        artist.UpdateName(name);
        await SaveAsync();
    }

    
}