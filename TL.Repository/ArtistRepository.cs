using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;

namespace TL.Repository;

public interface IArtistRepository : IGenericRepository<Artist>
{
    Task AddArtist(Artist artist);
    Task DeleteArtist(int id);
    new Task<Artist> FindAsync (int id);
    Task<IEnumerable<Artist>> GetAllArtists();
    Task<IEnumerable<Artist>> GetByExactName(string name);
    Task UpdateName(int id, string name);

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

    public override async Task<Artist> FindAsync (int id)
    {
        var result = await Context.Artists
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (result == null)
            throw new InvalidOperationException("Artist not found");

        return result;
    }
    
    public async Task<IEnumerable<Artist>> GetAllArtists()
    {
        return await GetEntities().ToListAsync();
    }

    public async Task<IEnumerable<Artist>> GetByExactName(string name)
    {
        return await GetByWhere(artist => artist.Name == name).ToListAsync();
    }

    public async Task UpdateName(int id, string name)
    {
        var artist = await FindAsync(id);
        artist.UpdateName(name);
        await SaveAsync();
    }

    
}