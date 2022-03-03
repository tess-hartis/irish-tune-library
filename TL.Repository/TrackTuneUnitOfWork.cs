using TL.Data;

namespace TL.Repository;

public interface ITrackTuneUnitOfWork
{
    Task Save();
    ITrackRepository TrackRepo { get; }
    ITuneRepository TuneRepo { get; }
}

public class TrackTuneUnitOfWork : ITrackTuneUnitOfWork
{
    private readonly TuneLibraryContext _context;
    private ITrackRepository _trackRepository;
    private ITuneRepository _tuneRepository;

    public TrackTuneUnitOfWork(TuneLibraryContext context)
    {
        _context = context;
    }
    
    public ITrackRepository TrackRepo
    {
        get
        {
            return _trackRepository = new TrackRepository(_context);
        }
    }

    public ITuneRepository TuneRepo
    {
        get
        {
            return _tuneRepository = new TuneRepository(_context);
        }
    }
    
    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}