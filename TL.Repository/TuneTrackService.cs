// using LanguageExt;
// using Microsoft.EntityFrameworkCore;
// using TL.Data;
// using TL.Domain;
// using TL.Domain.Exceptions;
// using TL.Domain.ValueObjects.TrackTuneValueObjects;
//
// namespace TL.Repository;
//
// public interface ITuneTrackService
// {
//     Task<TrackTune> AddExistingTuneToTrack(Track track, Tune tune, TrackTuneOrder trackTune);
//     // Task<Unit> RemoveTuneFromTrack(Track track, TrackTune trackTune);
//     Task<IEnumerable<Track>> FindTracksByTune(Tune tune);
// }
//
// public class TuneTrackService : ITuneTrackService
// {
//     private readonly TuneLibraryContext _context;
//     private readonly ITrackTuneRepository _trackTuneRepository;
//     
//
//     public TuneTrackService(TuneLibraryContext context, ITrackTuneRepository trackTuneRepository)
//     {
//         _context = context;
//         _trackTuneRepository = trackTuneRepository;
//        
//     }
//
//     private async Task SaveChangesAsync()
//     {
//         await _context.SaveChangesAsync();
//     }
//     
//     //
//     // public async Task<TrackTune> AddExistingTuneToTrack(Track track, Tune tune, TrackTuneOrder order)
//     // {
//     //     var trackTune = TrackTune.Create(track, tune, order);
//     //     track.AddTune(trackTune);
//     //     trackTune.SetTrackId(track.Id);
//     //     trackTune.SetTuneId(tune.Id);
//     //     trackTune.SetTitle(tune.Title.Value);
//     //     
//     //     await SaveChangesAsync();
//     //     return trackTune;
//     // }
//
//     // public async Task<Unit> RemoveTuneFromTrack(Track track, TrackTune trackTune)
//     // {
//     //     await _trackTuneRepository.DeleteAsync(trackTune);
//     //     await SaveChangesAsync();
//     //     return Unit.Default;
//     // }
//     //
//
//     public async Task<IEnumerable<Track>> FindTracksByTune(Tune tune)
//     {
//         var trackTunes = await _trackTuneRepository
//             .GetByWhere(x => x.TuneId == tune.Id).ToListAsync();
//
//         var tracks = trackTunes.Select(t => t.Track);
//
//         return tracks;
//     }
//     
// }