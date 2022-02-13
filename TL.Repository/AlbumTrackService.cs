using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;
using TL.Domain.ValueObjects.TrackValueObjects;

namespace TL.Repository;

public interface IAlbumTrackService
{
    Task<Track> AddNewTrackToAlbum(Album album, Track track);
    // Task RemoveTrackFromAlbum(int albumId, int trackId);
    Task<IEnumerable<Track>> GetAlbumTracks(Album album);

}
public class AlbumTrackService : IAlbumTrackService
{
    private readonly TuneLibraryContext _context;
    private readonly ITrackRepository _trackRepository;

    public AlbumTrackService(TuneLibraryContext context, ITrackRepository trackRepository)
    {
        _context = context;
        _trackRepository = trackRepository;
    }
    private async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<Track> AddNewTrackToAlbum(Album album, Track track)
    {
        track.SetAlbumId(album.Id);
        await _trackRepository.AddAsync(track);
        await SaveChangesAsync();
        return track;
    }
    

    // public async Task RemoveTrackFromAlbum(int albumId, int trackId)
    // {
    //     var album = await _albumRepository.FindAsync(albumId);
    //     var track = await _trackRepository.FindAsync(trackId);
    //     album.RemoveTrack(track);
    //     await SaveChangesAsync();
    // }

    public async Task<IEnumerable<Track>> GetAlbumTracks(Album album)
    {
        return await _trackRepository
            .GetByWhere(x => x.AlbumId == album.Id)
            .Include(x => x.Album)
            .OrderBy(x => x.TrackNumber.Value)
            .ToListAsync();
    }
}