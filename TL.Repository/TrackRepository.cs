using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;
using TL.Domain.Exceptions;
using TL.Domain.Validators;

namespace TL.Repository;

public interface ITrackRepository : IGenericRepository<Track>
{
    new Task<Track> FindAsync(int id);
    Task UpdateTrack(int id, string title, int trackNumber);
}

public class TrackRepository : GenericRepository<Track>, ITrackRepository
{
    public TrackRepository(TuneLibraryContext context) : base(context)
    {

    }

    private readonly TrackValidator _validator = new TrackValidator();
    
    public override async Task<Track> FindAsync(int id)
    {
        var track = await Context.Tracks
            .Include(t => t.TrackTunes)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (track == null)
            throw new InvalidOperationException($"Track with ID '{id}' was not found");
    
        return track;
    }

    public async Task UpdateTrack(int id, string title, int trackNumber)
    {
        var track = await FindAsync(id);
        track.Update(title, trackNumber);
        await SaveAsync();
    }
    
}