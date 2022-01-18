using TL.Data;
using TL.Domain;

namespace TL.Repository;

public interface IUnitOfWork
{
    ITuneRepository TuneRepository { get; }
    ITrackRepository TrackRepository { get; }
    IAlbumRepository AlbumRepository { get; }
    IArtistRepository ArtistRepository { get; }
    Task SaveChangesAsync();
    Task AddTuneToTrack(int tuneId, int trackId);
    Task AddTrackToAlbum(int trackId, int albumId);
    Task AddArtistToAlbum(int artistId, int albumId);
    Task<Album> GetAlbumByTrack(int trackId);
    Task<IEnumerable<Track>> GetByTuneFeatured(int tuneId);

}

public class UnitOfWork : IUnitOfWork
{
    private readonly TuneLibraryContext _context;
    public UnitOfWork(TuneLibraryContext context)
    {
        _context = context;
    }

    private ITuneRepository _tuneRepository;
    public ITuneRepository TuneRepository
    {
        get
        {
            return _tuneRepository = new TuneRepository(_context);
        }
    }

    private ITrackRepository _trackRepository;
    public ITrackRepository TrackRepository
    {
        get
        {
            return _trackRepository = new TrackRepository(_context);
        }
    }

    private IAlbumRepository _albumRepository;
    public IAlbumRepository AlbumRepository
    {
        get
        {
            return _albumRepository = new AlbumRepository(_context);
        }
    }
    
    private IArtistRepository _artistRepository;
    public IArtistRepository ArtistRepository
    {
        get
        {
            return _artistRepository = new ArtistRepository(_context);
        }
    }
    public async Task SaveChangesAsync()
    {
       await _context.SaveChangesAsync();
    }
    public async Task AddTuneToTrack(int tuneId, int trackId)
    {
        var tune = await _tuneRepository.FindAsync(tuneId);
        var track = await _trackRepository.FindAsync(trackId);
        track.AddTune(tune);
        await SaveChangesAsync();
       
        
    }
    public async Task AddTrackToAlbum(int trackId, int albumId)
    {
        var track = await _trackRepository.FindAsync(trackId);
        var album = await _albumRepository.FindAsync(albumId);
        album.AddTrack(track);
        await SaveChangesAsync();
            
    }
    public async Task AddArtistToAlbum(int artistId, int albumId)
    {
        var artist = await _artistRepository.FindAsync(artistId);
        var album = await _albumRepository.FindAsync(albumId);
        album.AddArtist(artist);
        await SaveChangesAsync();
        
    }

    public async Task<Album> GetAlbumByTrack(int trackId)
    {
        var track = await _trackRepository.FindAsync(trackId);
        var album = await _albumRepository.FindByTrackFeatured(track);
        if (album == null)
        {
            throw new Exception();
        }

        return album;
    }

    public async Task<IEnumerable<Track>> GetByTuneFeatured(int tuneId)
    {
        var tune = await _tuneRepository.FindAsync(tuneId);
        var tracks = await _trackRepository.FindByTuneFeatured(tune);
        return tracks;
    }
    
    
}