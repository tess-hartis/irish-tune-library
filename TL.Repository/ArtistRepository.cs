using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;

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

    public async Task UpdateArtist(int id, string name)
    {
        var artist = await FindAsync(id);
        artist.UpdateName(name);
        await SaveAsync();
    }

    
}