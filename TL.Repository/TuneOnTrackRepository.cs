using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;

namespace TL.Repository;

public interface ITuneOnTrackRepository : IGenericRepository<TuneOnTrack>
{
    
}

public class TuneOnTrackRepository : GenericRepository<TuneOnTrack>, ITuneOnTrackRepository
{
    public TuneOnTrackRepository(TuneLibraryContext context) : base(context)
    {
        
    }

    // public override async Task<TuneOnTrack> FindAsync(int id)
    // {
    //     var tuneOnTrack = await Context.TuneOnTracks
    //         .FirstOrDefaultAsync(x => x.Id == id);
    // }
}