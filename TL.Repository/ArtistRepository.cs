using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;
using TL.Domain.ValueObjects.ArtistValueObjects;

namespace TL.Repository;

public interface IArtistRepository : IGenericRepository<Artist>
{
    Task<Artist> UpdateArtist(int id, ArtistName name);
    Task<IEnumerable<Artist>> GetAllArtists();
}

public class ArtistRepository : GenericRepository<Artist>, IArtistRepository
{
    public ArtistRepository(TuneLibraryContext context) : base(context)
    {
       
    }

    public async Task<Artist> UpdateArtist(int id, ArtistName name)
    {
        var artist = await FindAsync(id);
        artist.UpdateName(name);
        await SaveAsync();
        return artist;
    }

    public async Task<IEnumerable<Artist>> GetAllArtists()
    {
        return await Context.Artists.Include(a => a.Albums).ToListAsync();
    }


}