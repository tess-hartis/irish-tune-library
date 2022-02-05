using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;

namespace TL.Repository;

public interface ITrackTuneRepository : IGenericRepository<TrackTune>
{
    
}

public class TrackTuneRepository : GenericRepository<TrackTune>, ITrackTuneRepository
{
    public TrackTuneRepository(TuneLibraryContext context) : base(context)
    {
        
    }
    
}