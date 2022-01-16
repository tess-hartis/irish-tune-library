using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;

namespace TL.Repository;

public interface IArtistRepository : IGenericRepository<Artist>
{
    Task Add(Artist artist);
    Task Delete(int id);
    Task<Artist> Find(int id);
    Task Update(int id);
    Task<IEnumerable<Artist>> GetAllArtists();
    Task<IEnumerable<Artist>> GetByName(string name);

}

public class ArtistRepository : GenericRepository<Artist>, IArtistRepository
{
    public ArtistRepository(TuneLibraryContext context) : base(context)
    {
       
    }

    public async Task Add(Artist artist)
    {
        await AddAsync(artist);
       
    }

    public async Task Delete(int id)
    {
        var artist = await FindAsync(id);
        await DeleteAsync(artist);
        
    }

    public async Task<Artist> Find(int id)
    {
        var result = await FindAsync(id);
        if (result == null)
        {
            throw new NullReferenceException();
        }

        return result;
    }

    public async Task Update(int id)
    {
        var artist = await FindAsync(id);
        await UpdateAsync(artist);
    }

    public async Task<IEnumerable<Artist>> GetAllArtists()
    {
        return await GetAll().ToListAsync();
    }

    public async Task<IEnumerable<Artist>> GetByName(string name)
    {
        return await GetByWhere(artist => artist.Name == name).ToListAsync();
    }
    
}