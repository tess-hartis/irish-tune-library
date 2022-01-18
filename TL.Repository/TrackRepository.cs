using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;

namespace TL.Repository;

public interface ITrackRepository : IGenericRepository<Track>
{
    Task Add(Track track);
    Task Delete(int id);
    Task<Track> FindAsync(int id);
    Task Update(int id);
    Task<IEnumerable<Track>> FindByExactTitleAsync(string title);
    Task<IEnumerable<Track>> FindByTuneFeatured(Tune tune);
    Task<IEnumerable<Track>> GetAllTracks();

}

public class TrackRepository : GenericRepository<Track>, ITrackRepository
{
    public TrackRepository(TuneLibraryContext context) : base(context)
    {

    }

    public async Task Add(Track track)
    {
        await AddAsync(track);
    }

    public async Task Delete(int id)
    {
        var track = await FindAsync(id);
        await DeleteAsync(track);
    }

    public override async Task<Track> FindAsync(int id)
    {
        var track = await Context.Tracks
            .Include(t => t.TuneList)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (track == null)
        {
            throw new Exception();
        }

        return track;
    }

    public async Task Update(int id)
    {
        var track = await FindAsync(id);
        await UpdateAsync(track);
    }

    public async Task<IEnumerable<Track>> FindByExactTitleAsync(string title)
    {
        return await GetByWhere(t => t.Title == title).ToListAsync();
    }

    public async Task<IEnumerable<Track>> FindByTuneFeatured(Tune tune)
    {
        return await Context.Tracks
            .Where(t => t.TuneList.Contains(tune))
            .ToListAsync();
    }

    public async Task<IEnumerable<Track>> GetAllTracks()
    {
        return await GetAll().ToListAsync();
    }

}