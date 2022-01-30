using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;

namespace TL.Repository;

public interface ITrackRepository : IGenericRepository<Track>
{
    new Task<Track> FindAsync(int id);
    Task<bool> UpdateTrack(Track track, string title, int trackNumber);
}

public class TrackRepository : GenericRepository<Track>, ITrackRepository
{
    public TrackRepository(TuneLibraryContext context) : base(context)
    {

    }

    public override async Task<Track> FindAsync(int id)
    {
        var track = await Context.Tracks.Include(t => t.TuneList)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (track == null)
            throw new InvalidOperationException("Track not found");

        return track;
    }

    public async Task<bool> UpdateTrack(Track track, string title, int trackNumber)
    {
        track.UpdateTitle(title);
        track.UpdateTrackNumber(trackNumber);
        return await SaveAsync() > 0;
    }
    
}