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
    Task AddExistingTuneToTrack(int tuneId, int trackId);
    Task<IEnumerable<Track>> FindTracksByTune(int tuneId);
    Task<IEnumerable<Album>> FindAlbumsByArtist(int artistId);


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
    public async Task AddExistingTuneToTrack(int tuneId, int trackId)
    {
        var tune = await _tuneRepository.FindAsync(tuneId);
        var track = await _trackRepository.FindAsync(trackId);
        track.AddExistingTune(tune);
        await SaveChangesAsync();
        
    }
    public async Task<IEnumerable<Track>> FindTracksByTune(int tuneId)
    {
        var tune = await _tuneRepository.FindAsync(tuneId);
        var tracks = await _trackRepository.FindByTuneFeatured(tune);
        return tracks;
    }

    public async Task<IEnumerable<Artist>> GetAlbumArtists(int albumId)
    {
        var album = await _albumRepository.FindAsync(albumId);
        var artists = await _artistRepository.FindAlbumArtists(album);
        return artists;
    }


}