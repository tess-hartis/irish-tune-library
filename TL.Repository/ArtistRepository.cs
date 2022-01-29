using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;

namespace TL.Repository;

public interface IArtistRepository : IGenericRepository<Artist>
{
    Task AddArtist(Artist artist);
    Task DeleteArtist(int id);
    Task<Artist> FindArtist (int id);
    Task<IEnumerable<Artist>> GetAllArtists();
    Task UpdateArtist(int id, string name);
}

public class ArtistRepository : GenericRepository<Artist>, IArtistRepository
{
    public ArtistRepository(TuneLibraryContext context) : base(context)
    {
       
    }

    public async Task AddArtist(Artist artist)
    {
        await AddAsync(artist);
    }

    public async Task DeleteArtist(int id)
    {
        var artist = await FindAsync(id);
        await DeleteAsync(artist);
    }

    public async Task<Artist> FindArtist (int id)
    {
        var artist = await FindAsync(id);
        
        if (artist == null)
            throw new InvalidOperationException("Artist not found");

        return artist;
    }
    
    public async Task<IEnumerable<Artist>> GetAllArtists()
    {
        return await GetEntities().ToListAsync();
    }
    
    public async Task UpdateArtist(int id, string name)
    {
        var artist = await FindAsync(id);
        artist.UpdateName(name);
        await SaveAsync();
    }

    
}