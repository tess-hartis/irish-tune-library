using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;

namespace TL.Repository;

public interface IArtistRepository : IGenericRepository<Artist>
{
    Task<bool> UpdateArtist(Artist artist, string name);
}

public class ArtistRepository : GenericRepository<Artist>, IArtistRepository
{
    public ArtistRepository(TuneLibraryContext context) : base(context)
    {
       
    }

    public async Task<bool> UpdateArtist(Artist artist, string name)
    {
        artist.UpdateName(name);
        return await SaveAsync() > 0;
    }

    
}