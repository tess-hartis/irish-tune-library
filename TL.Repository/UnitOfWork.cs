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
    Task AddNewTuneToTrack(int trackId, string title,
        TuneTypeEnum type, TuneKeyEnum key, string composer);
    Task RemoveTuneFromTrack(int trackId, int tuneId);
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
    public async Task AddNewTuneToTrack(int trackId, string title,
        TuneTypeEnum type, TuneKeyEnum key, string composer)
    {
        var track = await _trackRepository.FindAsync(trackId);
        track.AddNewTune(title, type, key, composer);
        await SaveChangesAsync();
    }
    public async Task AddExistingTuneToTrack(int tuneId, int trackId)
    {
        var tune = await _tuneRepository.FindAsync(tuneId);
        var track = await _trackRepository.FindAsync(trackId);
        track.AddExistingTune(tune);
        await SaveChangesAsync();
        
    }
    public async Task RemoveTuneFromTrack(int trackId, int tuneId)
    {
        var track = await _trackRepository.FindAsync(trackId);
        var tune = await _tuneRepository.FindAsync(tuneId);
        track.RemoveTune(tune.Id);
        await SaveChangesAsync();
    }
    public async Task<IEnumerable<Track>> FindTracksByTune(int tuneId)
    {
        var tune = await _tuneRepository.FindAsync(tuneId);
        var tracks = await _trackRepository.FindByTuneFeatured(tune);
        return tracks;
    }
    public async Task<IEnumerable<Album>> FindAlbumsByArtist(int artistId)
    {
        var artist = await _artistRepository.FindAsync(artistId);
        var albums = await _albumRepository.FindByArtist(artist);
        return albums;
    }


}