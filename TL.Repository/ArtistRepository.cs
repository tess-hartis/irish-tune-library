using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;
using TL.Domain.ValueObjects.ArtistValueObjects;

namespace TL.Repository;

public interface IArtistRepository : IGenericRepository<Artist>
{
    Task<IEnumerable<Artist>> GetAllArtists();
}

public class ArtistRepository : GenericRepository<Artist>, IArtistRepository
{
    public ArtistRepository(TuneLibraryContext context) : base(context)
    {
       
    }

    public async Task<IEnumerable<Artist>> GetAllArtists()
    {
        return await Context.Artists.Include(a => a.Albums).ToListAsync();
    }


}