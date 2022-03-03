using LanguageExt;
using LanguageExt.SomeHelp;
using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;

namespace TL.Repository;

public interface ITrackRepository : IGenericRepository<Track>
{
    new Task<Option<Track>> FindAsync(int id);
    Task<IEnumerable<Track>> GetAll();
}

public class TrackRepository : GenericRepository<Track>, ITrackRepository
{
    public TrackRepository(TuneLibraryContext context) : base(context)
    {

    }

    public override async Task<Option<Track>> FindAsync(int id)
    {
        var track = await Context.Tracks
            .Include(t => t.TrackTunes)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (track == null)
            return Option<Track>.None;

        return track.ToSome();
    }

    public async Task<IEnumerable<Track>> GetAll()
    {
        return await Context.Tracks.Include(t => t.TrackTunes).ToListAsync();
    }
}