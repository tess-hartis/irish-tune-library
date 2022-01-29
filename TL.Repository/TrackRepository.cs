using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;

namespace TL.Repository;

public interface ITrackRepository : IGenericRepository<Track>
{
    Task AddTrack(Track track);
    Task DeleteTrack(int id);
    Task<Track> FindTrack(int id);
    Task<IEnumerable<Track>> GetAllTracks();
    Task UpdateTrack(int id, string title, int trackNumber);
}

public class TrackRepository : GenericRepository<Track>, ITrackRepository
{
    public TrackRepository(TuneLibraryContext context) : base(context)
    {

    }

    public async Task AddTrack(Track track)
    {
        await AddAsync(track);
    }

    public async Task DeleteTrack(int id)
    {
        var track = await FindAsync(id);
        await DeleteAsync(track);
    }

    public async Task<Track> FindTrack(int id)
    {
        var track = await FindAsync(id);

        if (track == null)
            throw new InvalidOperationException("Track not found");

        return track;
    }
    
    public async Task<IEnumerable<Track>> GetAllTracks()
    {
        return await GetEntities().ToListAsync();
    }

    public async Task UpdateTrack(int id, string title, int trackNumber)
    {
        var track = await FindAsync(id);
        track.UpdateTitle(title);
        track.UpdateTrackNumber(trackNumber);
        await SaveAsync();
    }
    
}