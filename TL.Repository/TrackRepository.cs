using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;
using TL.Domain.Exceptions;
using TL.Domain.Validators;

namespace TL.Repository;

public interface ITrackRepository : IGenericRepository<Track>
{
    new Task<Track> FindAsync(int id);
    Task UpdateTrack(Track track, string title, int trackNumber);
}

public class TrackRepository : GenericRepository<Track>, ITrackRepository
{
    public TrackRepository(TuneLibraryContext context) : base(context)
    {

    }

    private readonly TrackValidator Validator = new TrackValidator();
    
    public override async Task<Track> FindAsync(int id)
    {
        var track = await Context.Tracks.Include(t => t.TuneList)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (track == null)
            throw new InvalidOperationException("Track not found");

        return track;
    }

    public async Task UpdateTrack(Track track, string title, int trackNumber)
    {
        track.UpdateTitle(title);
        track.UpdateTrackNumber(trackNumber);

        var errors = new List<string>();
        var results = await Validator.ValidateAsync(track);
        if (results.IsValid == false)
        {
            foreach (var validationFailure in results.Errors)
            {
                errors.Add($"{validationFailure.ErrorMessage}");
            }
            
            throw new InvalidEntityException(string.Join(", ", errors));
        }

        await SaveAsync();
    }
    
}