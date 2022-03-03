using TL.Data;

namespace TL.Repository;

public interface IAlbumArtistUnitOfWork
{
    Task Save();
    IAlbumRepository AlbumRepo { get; }
    IArtistRepository ArtistRepo { get; }
}
public class AlbumArtistUnitOfWork : IAlbumArtistUnitOfWork
{
    private readonly TuneLibraryContext _context;
    private IAlbumRepository _albumRepository;
    private IArtistRepository _artistRepository;

    public AlbumArtistUnitOfWork(TuneLibraryContext context)
    {
        _context = context;
    }
    
    public IAlbumRepository AlbumRepo
    {
        get
        {
            return _albumRepository = new AlbumRepository(_context);
        }
    }

    public IArtistRepository ArtistRepo
    {
        get
        {
            return _artistRepository = new ArtistRepository(_context);
        }
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}