using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;
using TL.Domain.ValueObjects.TrackValueObjects;

namespace TL.Repository;

public interface IAlbumTrackService
{
    Task AddNewTrackToAlbum(int albumId, TrackTitle title, TrackNumber trackNumber);
    Task RemoveTrackFromAlbum(int albumId, int trackId);
    Task<IEnumerable<Track>> GetAlbumTracks(int albumId);

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

    public async Task AddNewTrackToAlbum(int albumId, TrackTitle title, TrackNumber trackNumber)
    {
        var album = await _albumRepository.FindAsync(albumId);
        var track = Track.CreateTrack(title, trackNumber);
        track.SetAlbumId(album.Id);
        await _trackRepository.AddAsync(track);
        await SaveChangesAsync();
    }

    public async Task RemoveTrackFromAlbum(int albumId, int trackId)
    {
        var album = await _albumRepository.FindAsync(albumId);
        var track = await _trackRepository.FindAsync(trackId);
        album.RemoveTrack(track);
        await SaveChangesAsync();
    }

    public async Task<IEnumerable<Track>> GetAlbumTracks(int albumId)
    {
        return await _trackRepository
            .GetByWhere(x => x.AlbumId == albumId)
            .Include(x => x.Album)
            .OrderBy(x => x.TrackNumber)
            .ToListAsync();
    }
}