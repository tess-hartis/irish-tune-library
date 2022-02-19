// using System.Diagnostics;
// using Microsoft.EntityFrameworkCore;
// using TL.Data;
// using TL.Domain;
// using TL.Domain.ValueObjects.TrackValueObjects;
//
// namespace TL.Repository;
//
// public interface IAlbumTrackService
// {
//     Task<Track> AddNewTrackToAlbum(Album album, Track track);
//     // Task RemoveTrackFromAlbum(int albumId, int trackId);
//     // Task<IEnumerable<Track>> GetAlbumTracks(Album album);
//
// }
// public class AlbumTrackService : IAlbumTrackService
// {
//     private readonly TuneLibraryContext _context;
//     private ITrackRepository _trackRepository;
//     private IAlbumRepository _albumRepository;
//
//     public AlbumTrackService(TuneLibraryContext context)
//     {
//         _context = context;
//     }
//
//     private ITrackRepository TrackRepo
//     {
//         get
//         {
//             return _trackRepository = new TrackRepository(_context);
//         }
//     }
//
//     private IAlbumRepository AlbumRepo
//     {
//         get
//         {
//             return _albumRepository = new AlbumRepository(_context);
//         }
//     }
//     
//     private async Task SaveChangesAsync()
//     {
//         await _context.SaveChangesAsync();
//     }
//
//     public async Task<Track> AddNewTrackToAlbum(int albumId, string trackTitle, int trackNumber)
//     {
//         var album = await AlbumRepo.FindAsync(albumId);
//         var title = Some(TrackTitle.Create(trackTitle));
//         var trackNumber = Some(TrkNumber.Create(command.TrackNumber));
//
//         var track =
//             from tt in title
//             from tn in trackNumber
//             select (tt, tn).Apply(((x, y) => Track.Create(x, y)))
//                 .Map(t => album
//                     .Map(a => 
//                         a.AddTrack(t)));
//         
//         
//         ignore(track.MapT(x => 
//             x.Map(async y => await _trackRepository.SaveAsync())));
//
//         return track;
//     }
//     
//
//     public async Task RemoveTrackFromAlbum(int albumId, int trackId)
//     {
//         var album = await _albumRepository.FindAsync(albumId);
//         var track = await _trackRepository.FindAsync(trackId);
//         album.RemoveTrack(track);
//         await SaveChangesAsync();
//     }
//
//     public async Task<IEnumerable<Track>> GetAlbumTracks(Album album)
//     {
//         return await _trackRepository
//             .GetByWhere(x => x.AlbumId == album.Id)
//             .Include(x => x.Album)
//             .OrderBy(x => x.TrackNumber.Value)
//             .ToListAsync();
//     }
// }