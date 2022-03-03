using TL.Data;

namespace TL.Repository;

public interface ITrackTuneUnitOfWork
{
    Task Save();
    ITrackRepository TrackRepo { get; }
    ITuneRepository TuneRepo { get; }
    ITrackTuneRepository TrackTuneRepo { get; }
}

public class TrackTuneUnitOfWork : ITrackTuneUnitOfWork
{
    private readonly TuneLibraryContext _context;
    private ITrackRepository _trackRepository;
    private ITuneRepository _tuneRepository;
    private ITrackTuneRepository _trackTuneRepository;

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

    public ITrackTuneRepository TrackTuneRepo
    {
        get
        {
            return _trackTuneRepository = new TrackTuneRepository(_context);
        }
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}