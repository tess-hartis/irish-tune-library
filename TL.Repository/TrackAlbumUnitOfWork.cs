using TL.Data;

namespace TL.Repository;

public interface ITrackAlbumUnitOfWork
{
    Task Save();
    ITrackRepository TrackRepo { get; }
    IAlbumRepository AlbumRepo { get; }
}
public class TrackAlbumUnitOfWork : ITrackAlbumUnitOfWork
{
    private readonly TuneLibraryContext _context;
    private ITrackRepository _trackRepository;
    private  IAlbumRepository _albumRepository;

    public TrackAlbumUnitOfWork(TuneLibraryContext context)
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

    public IAlbumRepository AlbumRepo
    {
        get
        {
            return _albumRepository = new AlbumRepository(_context);
        }
    }
    
    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}