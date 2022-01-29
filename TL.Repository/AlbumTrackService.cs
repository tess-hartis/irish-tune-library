using TL.Data;
using TL.Domain;

namespace TL.Repository;

public interface IAlbumTrackService
{
    Task AddNewTrackToAlbum(int albumId, string title, int trackNumber);
    // Task RemoveTrackFromAlbum(int albumId, int trackId);

}
public class AlbumTrackService : IAlbumTrackService
{
    private readonly TuneLibraryContext _context;
    private readonly IAlbumRepository _albumRepository;
    private readonly ITrackRepository _trackRepository;

    public AlbumTrackService(TuneLibraryContext context, IAlbumRepository albumRepository, ITrackRepository trackRepository)
    {
        _context = context;
        _albumRepository = albumRepository;
        _trackRepository = trackRepository;
    }
    private async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task AddNewTrackToAlbum(int albumId, string title, int trackNumber)
    {
        var album = await _albumRepository.FindAsync(albumId);
        var track = Track.CreateTrack(title, trackNumber);
        await _trackRepository.AddTrack(track);
        album.AddTrack(track);
        await SaveChangesAsync();
    }

    // public async Task RemoveTrackFromAlbum(int albumId, int trackId)
    // {
    //     var album = await _albumRepository.FindAsync(albumId);
    //     var track = await _trackRepository.FindAsync(trackId);
    //     album.RemoveTrack(track.Id);
    //     await SaveChangesAsync();
    // }
}